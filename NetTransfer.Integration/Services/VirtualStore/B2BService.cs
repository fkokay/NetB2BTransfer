using NetTransfer.B2B.Library;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Integration.Models;
using NetTransfer.Logo.Library.Models;
using NetTransfer.Netsis.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Services.VirtualStore
{
    public class B2BService(B2BClient _b2bClient)
    {
        public async Task<B2BResponse?> UpdateProductPrice(List<BaseMalzemeFiyatModel> malzemeFiyatList)
        {
            try
            {
                var b2BUrunFiyat = new B2BUrunFiyat
                {
                    kod = "LF",
                    baslik = "Liste Fiyat",
                    aciklama = "Liste Fiyat",
                    tarih_aralik_durum = 0,
                    baslangic_tarihi = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss"),
                    bitis_tarihi = DateTime.Now.AddDays(90).ToString("yyyy-MM-dd HH:mm:ss"),
                    durum = true,
                    urunler = malzemeFiyatList.Select(item => new B2BUrunFiyatItem
                    {
                        urun_kodu = item.StokKodu,
                        list_fiyati = Convert.ToDecimal(item.Fiyat),
                        doviz_kodu = "TRY"
                    }).ToList()
                };

                return await _b2bClient.UrunFiyatTrasnferAsync(b2BUrunFiyat);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("Error updating product prices", ex);
            }
        }
        public async Task UpdateProductPrice(List<MalzemeFiyatModel> malzemeFiyatList)
        {
            try
            {
                var groups = malzemeFiyatList.GroupBy(m => new { m.kod, m.kod_baslik, m.aciklama, m.tarih_aralik_durum, m.baslangic_tarihi, m.bitis_tarihi, m.durum }).ToList();
                foreach (var group in groups)
                {
                    var b2BUrunFiyat = new B2BUrunFiyat
                    {
                        kod = group.Key.kod,
                        baslik = group.Key.kod_baslik,
                        aciklama = group.Key.aciklama,
                        tarih_aralik_durum = group.Key.tarih_aralik_durum,
                        baslangic_tarihi = group.Key.baslangic_tarihi.ToString("yyyy-MM-dd HH:mm:ss"),
                        bitis_tarihi = group.Key.bitis_tarihi.ToString("yyyy-MM-dd HH:mm:ss"),
                        durum = group.Key.durum,
                        urunler = malzemeFiyatList.Where(m => m.kod == group.Key.kod).Select(item => new B2BUrunFiyatItem
                        {
                            urun_kodu = item.urun_kodu,
                            list_fiyati = Convert.ToDecimal(item.liste_fiyati),
                            doviz_kodu = item.doviz_kodu
                        }).ToList()
                    };

                    await _b2bClient.UrunFiyatTrasnferAsync(b2BUrunFiyat);
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("Error updating product prices", ex);
            }
        }

        public async Task<B2BResponse?> UpdateProductStock(List<BaseMalzemeStokModel> malzemeStokList)
        {
            try
            {
                B2BDepoMiktar b2BDepoMiktar = new B2BDepoMiktar
                {
                    data = malzemeStokList.Select(item => new B2BUrun
                    {
                        urun_kodu = item.StokKodu,
                        depolar = new List<B2BDepo>
                        {
                            new B2BDepo
                            {
                                depo_kodu = item.DepoKodu,
                                depo_baslik = item.DepoAdi,
                                miktar = Convert.ToInt32(item.StokMiktari),
                            }
                        }
                    }).ToList()
                };

                return await _b2bClient.UrunStokTransferAsync(b2BDepoMiktar);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("Error updating product stock", ex);
            }
        }

        public List<B2BUrun>? MappingProduct(string erp, object? malzemeList)
        {
            switch (erp)
            {
                case "Logo":
                    return MappingProductLogo(malzemeList as List<ItemModel>);
                case "Netsis":
                    return MappingProductNetsis(malzemeList as List<MalzemeModel>);
                case "Opak":
                    return MappingProductOpak(malzemeList as List<ItemModel>);
            }

            return null;
        }

        private List<B2BUrun> MappingProductOpak(List<ItemModel>? ıtemModels)
        {
            throw new NotImplementedException();
        }

        private List<B2BUrun> MappingProductNetsis(List<MalzemeModel>? malzemeler)
        {
            List<B2BUrun> urunList = new List<B2BUrun>();

            if (malzemeler == null)
                return urunList;

            foreach (var item in malzemeler)
            {
                B2BUrun urun = new B2BUrun
                {
                    marka = new B2BMarka { kod = item.marka_kod, baslik = item.marka_adi },
                    grup = new B2BGrup { kod = item.grup_kod, baslik = item.grup_baslik },
                    birim = new B2BBirim { kod = item.birim_kodu, baslik = item.birim_baslik },
                    barkod_no = item.barkod_no,
                    urun_kodu = item.urun_kodu,
                    doviz_kodu = item.doviz_kodu,
                    baslik = item.baslik,
                    aciklama = item.aciklama,
                    icerik = item.icerik,
                    kdv_durumu = item.kdv_durumu,
                    kdv_orani = item.kdv_orani,
                    liste_fiyati = item.liste_fiyati,
                    durum = item.durum,
                    yeni_urun = item.yeni_urun,
                    yeni_urun_tarih = item.yeni_urun_tarih,
                    minimum_satin_alma_miktari = item.minimum_satin_alma_miktari,
                    sepete_eklenme_miktari = item.sepete_eklenme_miktari,
                    varyant_durumu = item.varyant_durumu,
                    asorti_durumu = item.asorti_durumu,
                    model_no = item.model_no,
                    varyant_1 = new B2BVaryant
                    {
                        kod = item.varyant_1_kod,
                        baslik = item.varyant_1_baslik,
                        deger_kod = item.varyant_1_deger,
                        deger_baslik = item.varyant_1_deger_baslik,
                    },
                    varyant_2 = new B2BVaryant
                    {
                        kod = item.varyant_2_kod,
                        baslik = item.varyant_2_baslik,
                        deger_kod = item.varyant_2_deger,
                        deger_baslik = item.varyant_2_deger_baslik
                    },
                    asorti_miktar = item.asorti_miktar,
                    asorti_kod = item.asorti_kod,
                    kategoriler = new List<B2BKategori>()
                };
                urunList.Add(urun);
            }

            return urunList;
        }
        private List<B2BUrun> MappingProductLogo(List<ItemModel>? itemList)
        {
            List<B2BUrun> urunList = new List<B2BUrun>();

            if (itemList == null)
                return urunList;

            foreach (var item in itemList)
            {
                B2BUrun urun = new B2BUrun();
                urun.marka = new B2BMarka() { kod = item.STGRPCODE, baslik = item.STGRPNAME };
                urun.grup = new B2BGrup() { kod = item.STGRPCODE, baslik = item.STGRPNAME };
                urun.birim = new B2BBirim() { kod = item.UNITCODE, baslik = item.UNITCODE };
                urun.barkod_no = item.BARCODE;
                urun.urun_kodu = item.CODE;
                urun.doviz_kodu = "TRY";
                urun.baslik = item.NAME;
                urun.aciklama = item.NAME3;
                urun.icerik = "";
                urun.kdv_durumu = "kdv_haric";
                urun.kdv_orani = item.VAT;
                urun.liste_fiyati = item.PRICE;
                urun.durum = item.ACTIVE == 0 ? item.EXTACCESSFLAGS == 6 || item.EXTACCESSFLAGS == 7 ? 1 : 0 : 0;
                urun.yeni_urun = "0";
                urun.yeni_urun_tarih = Convert.ToDateTime("2024-01-01");
                urun.minimum_satin_alma_miktari = 1;
                urun.sepete_eklenme_miktari = 1;
                urun.varyant_durumu = "yok";
                urun.asorti_durumu = "yok";
                urun.model_no = "";
                urun.varyant_1 = new B2BVaryant() { kod = "", baslik = "" };
                urun.varyant_2 = new B2BVaryant() { kod = "", baslik = "" };
                urun.asorti_miktar = 0;
                urun.asorti_kod = "";
                urun.kategoriler = new List<B2BKategori>();

                urunList.Add(urun);
            }

            return urunList;
        }
        public List<B2BMusteri>? MappingMusteri(string erp, object? musteriList)
        {
            switch (erp)
            {
                case "Logo":
                    return MappingMusteriLogo(musteriList as List<LogoMusteriModel>);
                case "Netsis":
                    return MappingMusteriNetsis(musteriList as List<CariModel>);
                case "Opak":
                    return null;
            }

            return null;
        }
        private List<B2BMusteri>? MappingMusteriLogo(List<LogoMusteriModel>? data)
        {
            List<B2BMusteri> musteriList = new List<B2BMusteri>();

            if (data == null)
                return null; ;

            foreach (var item in data)
            {
                var musteri = new B2BMusteri
                {
                    musteri_ozellik = "kurumsal",
                    unvan = item.DEFINITION_,
                    cari_kod = item.CODE,
                    adi = item.CUSTNAME,
                    soyadi = item.CUSTSURNAME,
                    telefon = "",
                    adres = item.ADDR1 + (string.IsNullOrEmpty(item.ADDR2) ? "" : " " + item.ADDR2),
                    il = item.CITY,
                    ilce = item.TOWN,
                    vergi_dairesi = item.TAXOFFICE,
                    vergi_no = item.TAXNR,
                    tc_no = item.TCKNO,
                    plasiyer = item.CYPHCODE,
                    depo_kodu = "",
                    erp_kodu = item.CODE,
                    odeme_sekilleri = "",
                    musteri_kosul_kodu = item.SPECODE,
                    grup_kodu = "TURKUAZ",
                    fiyat_listesi_kodu = "",
                    email = item.CODE.Replace("Ş", "S").Replace("Ğ", "G").Replace("İ", "I").Replace("Ü", "U").Replace("Ç", "C").Replace("Ö", "O")
                    .Replace("ş", "s").Replace("ğ", "g").Replace("ı", "i").Replace("ü", "u").Replace("ç", "c").Replace("ö", "o")
                    .Replace(",", "").Replace(";", "").Replace(":", "").Replace("/", "").Replace("\\", "").Replace("'", "").Replace("\"", "")
                    .Replace("<", "").Replace(">", "").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "") + "@turkuaz.com",
                    kullanici_adi = item.CODE,
                    sifre = item.CODE.Length > 6 ? item.CODE.Substring(6) : item.CODE,
                    email_durum_bildirimi = "H",
                    musteri_durumu = "1",
                };

                musteriList.Add(musteri);
            }

            return musteriList;
        }
        private List<B2BMusteri>? MappingMusteriNetsis(List<CariModel>? data)
        {
            List<B2BMusteri> musteriList = new List<B2BMusteri>();

            if (data == null)
                return null; ;

            foreach (var item in data)
            {
                var musteri = new B2BMusteri();
                musteri.il = item.il;
                musteri.ilce = item.ilce;
                musteri.musteri_ozellik = "kurumsal";
                musteri.unvan = item.unvan;
                musteri.cari_kod = item.cari_kod;
                musteri.adi = item.adi;
                musteri.soyadi = item.soyadi;
                musteri.telefon = item.telefon;
                musteri.adres = item.adres;
                musteri.vergi_dairesi = item.vergi_dairesi;
                musteri.vergi_no = item.vergi_no;
                musteri.tc_no = item.tc_no;
                musteri.plasiyer = item.plasiyer;
                musteri.depo_kodu = item.depo_kodu;
                musteri.erp_kodu = item.erp_kodu;
                musteri.odeme_sekilleri = item.odeme_sekilleri;
                musteri.musteri_kosul_kodu = item.musteri_kosul_kodu;
                musteri.grup_kodu = item.grup_kodu;
                musteri.fiyat_listesi_kodu = item.fiyat_listesi_kodu;
                musteri.email = item.email;
                musteri.kullanici_adi = item.kullanici_adi;
                musteri.sifre = item.sifre;
                musteri.email_durum_bildirimi = item.email_durum_bildirimi;
                musteri.musteri_durumu = item.musteri_durumu.ToString();



                musteriList.Add(musteri);
            }

            return musteriList;
        }
        public List<B2BMusteriBakiye>? MappingMusteriBakiye(string erp, object? musteriList)
        {
            switch (erp)
            {
                case "Logo":
                    return MappingMusteriBakiyeLogo(musteriList as List<LogoMusteriModel>);
                case "Netsis":
                    return MappingMusteriBakiyeNetsis(musteriList as List<CariBakiyeModel>);
                case "Opak":
                    return null;
            }

            return null;
        }
        private List<B2BMusteriBakiye>? MappingMusteriBakiyeLogo(List<LogoMusteriModel>? data)
        {
            List<B2BMusteriBakiye> musteriBakiyeList = new List<B2BMusteriBakiye>();

            if (data == null)
            {
                return null;
            }

            foreach (var item in data)
            {
                var musteriBakiye = new B2BMusteriBakiye
                {
                    cari_kod = item.CODE,
                    doviz_kodu = "TRY",
                    bakiye = Math.Round(item.BALANCE, 2),
                    gecikmis_gun = 0,
                    gecikmis_bakiye = 0,
                    borc_alacak_tipi = item.BALANCE > 0 ? "B" : "A",
                }
            ;

                musteriBakiyeList.Add(musteriBakiye);

            }

            return musteriBakiyeList;
        }
        private List<B2BMusteriBakiye>? MappingMusteriBakiyeNetsis(List<CariBakiyeModel>? data)
        {
            List<B2BMusteriBakiye> musteriBakiyeList = new List<B2BMusteriBakiye>();

            if (data == null)
            {
                return null;
            }

            foreach (var item in data)
            {
                var musteriBakiye = new B2BMusteriBakiye
                {
                    bakiye = item.bakiye,
                    borc_alacak_tipi = item.borc_alacak_tipi,
                    cari_kod = item.cari_kod,
                    doviz_kodu = item.doviz_kodu,
                    gecikmis_gun = item.gecikmis_gun,
                    gecikmis_bakiye = item.gecikmis_bakiye,
                };

                musteriBakiyeList.Add(musteriBakiye);

            }

            return musteriBakiyeList;
        }
    }
}
