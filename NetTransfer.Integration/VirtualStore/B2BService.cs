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

namespace NetTransfer.Integration.VirtualStore
{
    public class B2BService(B2BClient _b2bClient)
    {
        public async Task UpdateProductPrice(List<BaseMalzemeFiyatModel> malzemeFiyatList)
        {
            B2BUrunFiyat b2BUrunFiyat = new B2BUrunFiyat();
            b2BUrunFiyat.kod = "LF";
            b2BUrunFiyat.baslik = "Liste Fiyat";
            b2BUrunFiyat.aciklama = "Liste Fiyat";
            b2BUrunFiyat.tarih_aralik_durum = 0;
            b2BUrunFiyat.baslangic_tarihi = DateTime.Now.AddDays(-10);
            b2BUrunFiyat.bitis_tarihi = DateTime.Now.AddDays(90);
            b2BUrunFiyat.durum = true;
            b2BUrunFiyat.urunler = new List<B2BUrun>();
            foreach (var item in malzemeFiyatList)
            {
                b2BUrunFiyat.urunler.Add(new B2BUrun()
                {
                    urun_kodu = item.StokKodu,
                    liste_fiyati = item.Fiyat,
                    doviz_kodu = "TRY"
                });
            }

            var result = await _b2bClient.UrunFiyatTrasnferAsync(b2BUrunFiyat);
        }

        public async Task UpdateProductStock(List<BaseMalzemeStokModel> malzemeStokList)
        {
            B2BDepoMiktar b2BDepoMiktar = new B2BDepoMiktar();

            foreach (var item in malzemeStokList)
            {
                b2BDepoMiktar.data.Add(new B2BUrun()
                {
                    urun_kodu = item.StokKodu,
                    depolar = new List<B2BDepo>()
                    {
                        new B2BDepo()
                        {
                            depo_kodu="0",
                            depo_baslik = "Depo",
                            miktar = Convert.ToInt32(item.StokMiktari),
                        }
                    }
                });
            }

            var result = await _b2bClient.UrunStokTransferAsync(b2BDepoMiktar);
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

        private List<B2BUrun> MappingProductNetsis(List<MalzemeModel>? malzemeModels)
        {
            throw new NotImplementedException();
        }

        private List<B2BUrun> MappingProductLogo(List<ItemModel>? itemList)
        {
            List<B2BUrun> urunList = new List<B2BUrun>();
            foreach (var item in itemList)
            {
                B2BUrun urun = new B2BUrun();
                urun.marka = new B2BMarka() { kod = item.MARKNAME, baslik = item.MARKNAME };
                urun.grup = new B2BGrup() { kod = item.STGRPCODE, baslik = item.STGRPCODE };
                urun.birim = new B2BBirim() { kod = item.CYPHCODE, baslik = item.CYPHCODE };
                urun.barkod_no = item.BARCODE;
                urun.urun_kodu = item.CODE;
                urun.doviz_kodu = "TRY";
                urun.baslik = item.NAME;
                urun.aciklama = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                urun.icerik = "";
                urun.kdv_durumu = "kdv_haric";
                urun.kdv_orani = item.VAT;
                urun.liste_fiyati = item.PRICE;
                urun.durum = item.EXTACCESSFLAGS == 6 || item.EXTACCESSFLAGS == 7 ? 1 : 0;
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
                    return MappingMusteriLogo(musteriList as List<ArpModel>);
                case "Netsis":
                    return null;
                case "Opak":
                    return null;
            }

            return null;
        }

        private List<B2BMusteri>? MappingMusteriLogo(List<ArpModel>? data)
        {
            List<B2BMusteri> musteriList = new List<B2BMusteri>();
            foreach (var item in data)
            {
                var musteri = new B2BMusteri
                {
                    musteri_ozellik = "kurumsal",
                    unvan = item.DEFINITION_,
                    cari_kod = item.CODE,
                    adi = item.CUSTNAME,
                    soyadi = item.CUSTSURNAME,
                    telefon = item.TELNRS1,
                    adres = item.ADDR1 + " " + item.ADDR2,
                    il = item.CITY,
                    ilce = item.TOWN,
                    vergi_dairesi = item.TAXOFFICE,
                    vergi_no = item.TAXNR,
                    tc_no = item.TCKNO,
                    plasiyer = item.CYPHCODE,
                    depo_kodu = "",
                    erp_kodu = item.CODE,
                    odeme_sekilleri = new List<string>(),
                    musteri_kosul_kodu = item.SPECODE,
                    grup_kodu = "TURKUAZ",
                    fiyat_listesi_kodu = "",
                    email = item.CODE + "@turkuaz.com",
                    kullanici_adi = item.CODE,
                    sifre = item.CODE.Substring(6),
                    email_durum_bildirimi = "H",
                    musteri_durumu = "1",
                };

                musteriList.Add(musteri);
            }

            return musteriList;
        }

        public List<B2BMusteriBakiye>? MappingMusteriBakiye(string erp, object? musteriList)
        {
            switch (erp)
            {
                case "Logo":
                    return MappingMusteriBakiyeLogo(musteriList as List<ArpModel>);
                case "Netsis":
                    return null;
                case "Opak":
                    return null;
            }

            return null;
        }

        private List<B2BMusteriBakiye>? MappingMusteriBakiyeLogo(List<ArpModel>? data)
        {
            List<B2BMusteriBakiye> musteriBakiyeList = new List<B2BMusteriBakiye>();
            foreach (var item in data)
            {
                var musteriBakiye = new B2BMusteriBakiye
                {
                    cari_kod = item.CODE,
                    doviz_kodu = "TL",
                    bakiye = item.BALANCE,
                    gecikmis_gun = 0,
                    gecikmis_bakiye = 0,
                    borc_alacak_tipi = "B",
                };

                musteriBakiyeList.Add(musteriBakiye);

            }

            return musteriBakiyeList;
        }
    }
}
