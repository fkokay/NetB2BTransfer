using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Class
{
    public class LogoQuery
    {
        public static string GetArpQuery(LogoQueryParam param)
        {
            var offsetfilter = "";
            if (param.limit != "-1")
            {
                offsetfilter = " OFFSET " + param.offset + " ROWS \r\n" +
                               " FETCH NEXT " + param.limit + " ROWS ONLY;";
            }

            var query = "SELECT\r\n" +
                        "CLCARD.LOGICALREF,\r\n" +
                        "CLCARD.CODE,\r\n" +
                        "CLCARD.ACTIVE,\r\n" +
                        "CLCARD.DEFINITION_,\r\n" +
                        "CLCARD.DEFINITION2,\r\n" +
                        "CLCARD.CARDTYPE,\r\n" +
                        "CLCARD.SPECODE,\r\n" +
                        "CLCARD.SPECODE2,\r\n" +
                        "CLCARD.SPECODE3,\r\n" +
                        "CLCARD.SPECODE4,\r\n" +
                        "CLCARD.SPECODE5,\r\n" +
                        "CLCARD.CYPHCODE,\r\n" +
                        "CLCARD.ADDR1,\r\n" +
                        "CLCARD.ADDR2,\r\n" +
                        "CLCARD.DISTRICT,\r\n" +
                        "CLCARD.POSTCODE,\r\n" +
                        "CLCARD.TOWN,\r\n" +
                        "CLCARD.CITY,\r\n" +
                        "CLCARD.CITYCODE,\r\n" +
                        "CLCARD.COUNTRY,\r\n" +
                        "CLCARD.TELCODES1,\r\n" +
                        "CLCARD.TELNRS1,\r\n" +
                        "CLCARD.TELCODES2,\r\n" +
                        "CLCARD.TELNRS2,\r\n" +
                        "CLCARD.FAXCODE,\r\n" +
                        "CLCARD.FAXNR,\r\n" +
                        "CLCARD.INCHARGE, \r\n" +
                        "CLCARD.TAXOFFICE,\r\n" +
                        "CLCARD.TAXNR,\r\n" +
                        "CLCARD.TCKNO,\r\n" +
                        "CLCARD.ISPERSCOMP,\r\n" +
                        "CLCARD.EMAILADDR,\r\n" +
                        "CLCARD.WEBADDR,\r\n" +
                        "CLCARD.MAPID,\r\n" +
                        "CLCARD.LONGITUDE,\r\n" +
                        "CLCARD.LATITUTE,\r\n" +
                        "CLCARD.EDINO,\r\n" +
                        "CLCARD.CITYID,\r\n" +
                        "CLCARD.TOWNID,\r\n" +
                        "CLCARD.DISCRATE,\r\n" +
                        "CLCARD.NAME AS CUSTNAME,\r\n" +
                        "CLCARD.SURNAME AS CUSTSURNAME,\r\n" +
                        "CLCARD.TRADINGGRP, \r\n" +
                        "CLCARD.PAYMENTREF, \r\n" +
                        "ISNULL(PAYPLANS.CODE,'') AS PAYMENTCODE, \r\n" +
                        "CLCARD.POSTLABELCODE, \r\n" +
                        "CLCARD.SENDERLABELCODE, \r\n" +
                        "CLCARD.ACCEPTEINV, \r\n" +
                        "CLCARD.PROFILEID, \r\n" +
                        "CLCARD.PROJECTREF, \r\n" +
                        "CLCARD.BANKNAMES1 AS BANKNAME1, \r\n" +
                        "CLCARD.BANKIBANS1 AS BANKACCIBAN1, \r\n" +
                        "CLCARD.BANKNAMES2 AS BANKNAME2, \r\n" +
                        "CLCARD.BANKIBANS2 AS BANKACCIBAN2, \r\n" +
                        "CLCARD.BANKNAMES3 AS BANKNAME3, \r\n" +
                        "CLCARD.BANKIBANS3 AS BANKACCIBAN3, \r\n" +
                        "CLCARD.POSTLABELCODEDESP, \r\n" +
                        "CLCARD.SENDERLABELCODEDESP, \r\n" +
                        "CLCARD.ACCEPTEDESP, \r\n" +
                        "ISNULL((SELECT SUM(GNTOTCL.DEBIT) FROM " + LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTCL") + " AS GNTOTCL WITH(NOLOCK) WHERE CLCARD.LOGICALREF=GNTOTCL.CARDREF AND GNTOTCL.TOTTYP=1),0.00) AS DEBIT,\r\n" +
                        "ISNULL((SELECT SUM(GNTOTCL.CREDIT) FROM " + LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTCL") + " AS GNTOTCL WITH(NOLOCK) WHERE CLCARD.LOGICALREF=GNTOTCL.CARDREF AND GNTOTCL.TOTTYP=1),0.00) AS CREDIT,\r\n" +
                        "ISNULL((SELECT SUM(GNTOTCL.DEBIT)-SUM(GNTOTCL.CREDIT) FROM " + LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTCL") + " AS GNTOTCL WITH(NOLOCK) WHERE CLCARD.LOGICALREF=GNTOTCL.CARDREF AND GNTOTCL.TOTTYP=1),0.00) AS BALANCE \r\n" +
                        " FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "CLCARD") + " AS CLCARD WITH(NOLOCK)  \r\n" +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "PAYPLANS") + " AS PAYPLANS ON CLCARD.PAYMENTREF = PAYPLANS.LOGICALREF\r\n" +
                        " WHERE (CLCARD.CARDTYPE<>22) " + param.filter + "\r\n" +
                        " ORDER BY " + param.orderbyfieldname + " " + param.ascdesc + "" + offsetfilter + " ";

            return query;
        }
        public static string GetArpBalanceQuery(LogoQueryParam param)
        {
            var query = "SELECT\r\n" +
                        "CLCARD.LOGICALREF,\r\n" +
                        "CLCARD.CODE,\r\n" +
                        "ISNULL((SELECT SUM(GNTOTCL.DEBIT) FROM " + LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTCL") + " AS GNTOTCL WITH(NOLOCK) WHERE CLCARD.LOGICALREF=GNTOTCL.CARDREF AND GNTOTCL.TOTTYP=1),0.00) AS DEBIT,\r\n" +
                        "ISNULL((SELECT SUM(GNTOTCL.CREDIT) FROM " + LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTCL") + " AS GNTOTCL WITH(NOLOCK) WHERE CLCARD.LOGICALREF=GNTOTCL.CARDREF AND GNTOTCL.TOTTYP=1),0.00) AS CREDIT,\r\n" +
                        "ISNULL((SELECT SUM(GNTOTCL.DEBIT)-SUM(GNTOTCL.CREDIT) FROM " + LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTCL") + " AS GNTOTCL WITH(NOLOCK) WHERE CLCARD.LOGICALREF=GNTOTCL.CARDREF AND GNTOTCL.TOTTYP=1),0.00) AS BALANCE \r\n" +
                        " FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "CLCARD") + " AS CLCARD WITH(NOLOCK)  \r\n" +
                        " WHERE (CLCARD.CARDTYPE<>22) " + param.filter + "\r\n" +
                        " ORDER BY " + param.orderbyfieldname + " " + param.ascdesc;

            return query;
        }
        public static string GetArpCountQuery(LogoQueryParam param)
        {
            return "SELECT COUNT(CLCARD.LOGICALREF) FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "CLCARD") +
                   " CLCARD WITH (NOLOCK) WHERE (CLCARD.ACTIVE = 0) AND (CLCARD.CARDTYPE<>22) " + param.filter;
        }
        public static string GetItemsQuery(LogoQueryParam param)
        {
            var offsetfilter = "";
            if (param.limit != "-1")
            {
                offsetfilter = " OFFSET " + param.offset + " ROWS \r\n" +
                               " FETCH NEXT " + param.limit + " ROWS ONLY;";
            }

            var price = "";
            if (!string.IsNullOrEmpty(param.data))
            {
                price = JsonConvert.DeserializeObject<string>(param.data);

            }

            var coksatilanqueery = "";
            if (param.coksatilan == "CS")
            {

                coksatilanqueery = " AND (ITEMS.LOGICALREF IN(SELECT TOP 100  \r\n" +
                        " STL.STOCKREF \r\n" +
                        " FROM " + LogoUtils.TableNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "STLINE") +
                        " AS STL WITH (NOLOCK) \r\n" +
                        " WHERE (STL.TRCODE IN(7,8)) AND (STL.LINETYPE = 0) AND (STL.CANCELLED = 0)  \r\n" +
                        " GROUP BY STL.STOCKREF " +
                        " ORDER BY SUM(STL.AMOUNT) DESC)) ";
            }



            var query = "SELECT \r\n" +
                        "(SELECT COUNT(ITEMS.LOGICALREF) FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "ITEMS") +
                        " AS ITEMS WITH (NOLOCK) WHERE (ITEMS.ACTIVE = 0) " + param.filter + ") AS TOTALCOUNT,\r\n" +
                        "ITEMS.LOGICALREF,\r\n" +
                        "ITEMS.CODE,\r\n" +
                        "ITEMS.NAME,\r\n" +
                        "ITEMS.NAME2,\r\n" +
                        "ITEMS.NAME3,\r\n" +
                        "MARK.CODE AS MARKCODE,\r\n" +
                        "MARK.DESCR AS MARKNAME,\r\n" +
                        "ITEMS.STGRPCODE,\r\n" +
                        "(SELECT ISNULL(DEFINITION_,'DİĞER') FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "SPECODES") + " WHERE  (CODETYPE = 4) AND SPECODE =ITEMS.STGRPCODE) AS STGRPNAME,\r\n" +
                        "ITEMS.CYPHCODE,\r\n" +
                        "ITEMS.PRODUCERCODE,\r\n" +
                        "ITEMS.SPECODE,\r\n" +
                        "SPE.DEFINITION_ AS SPECODENAME,\r\n" +
                        "ITEMS.SPECODE2,\r\n" +
                        "ITEMS.SPECODE3,\r\n" +
                        "ITEMS.SPECODE4,\r\n" +
                        "ITEMS.SPECODE5,\r\n" +
                        "ITEMS.KEYWORD1,\r\n" +
                        "ITEMS.KEYWORD2,\r\n" +
                        "ITEMS.KEYWORD3,\r\n" +
                        "ITEMS.KEYWORD4,\r\n" +
                        "ITEMS.KEYWORD5,\r\n" +
                        "ITEMS.DEDUCTCODE,\r\n" +
                        "ITEMS.CANDEDUCT,\r\n" +
                        "ITEMS.SALEDEDUCTPART1,\r\n" +
                        "ITEMS.SALEDEDUCTPART2,\r\n" +
                        "ITEMS.TRACKTYPE,\r\n" +
                        "ITEMS.LOCTRACKING,\r\n" +
                        "ITEMS.VAT, \r\n" +
                        "ITEMS.SELLVAT, \r\n" +
                        "ITEMS.UNITSETREF,\r\n" +
                        "UNITSETL.LOGICALREF AS UOMREF,\r\n" +
                        "UNITSETL.CODE AS UNITCODE,\r\n" +
                        "ITEMS.EXTACCESSFLAGS,\r\n" +
                        "(SELECT TOP 1 UNITBCODE.BARCODE FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "UNITBARCODE") + " UNITBCODE WITH(NOLOCK) WHERE " +
                        "(ITEMREF = ITEMS.LOGICALREF AND UNITLINEREF = UNITSETL.LOGICALREF)) AS BARCODE," +
                         price +

                        "(SELECT ISNULL((SUM (GNSTITOT.ONHAND)-SUM (GNSTITOT.RESERVED)),0) AS STOCKAMOUNT FROM " +
                               LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTST") +
                               " GNSTITOT WITH(NOLOCK) WHERE (GNSTITOT.STOCKREF = ITEMS.LOGICALREF) AND (GNSTITOT.INVENNO = -1)) AS GNTOTSTOCK," +

                        "(SELECT ISNULL((SUM (GNSTITOT.ONHAND)-SUM (GNSTITOT.RESERVED)),0) AS STOCKAMOUNT FROM " +
                               LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTST") +
                               " GNSTITOT WITH(NOLOCK) WHERE (GNSTITOT.STOCKREF = ITEMS.LOGICALREF) AND (GNSTITOT.INVENNO IN (" + (string.IsNullOrEmpty(param.usstocknr) ? "-1" : param.usstocknr) + ")) ) AS USTOTSTOCK" +

                        " FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "ITEMS") + " AS ITEMS WITH(NOLOCK) \r\n" +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "UNITSETL") +
                        " AS UNITSETL WITH(NOLOCK) ON (UNITSETL.UNITSETREF=ITEMS.UNITSETREF)  \r\n" +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "MARK") +
                        " AS MARK WITH(NOLOCK) ON MARK.LOGICALREF = ITEMS.MARKREF " +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "SPECODES") + " AS SPE WITH(NOLOCK) ON ITEMS.SPECODE = SPE.SPECODE AND SPE.CODETYPE = 1 AND SPE.SPECODETYPE = 14 " +
                        " WHERE (ITEMS.ACTIVE=0) AND (UNITSETL.MAINUNIT=1) AND (ITEMS.CARDTYPE NOT IN(4,20,21)) " + coksatilanqueery + " " + param.filter + "  \r\n" +
                        " ORDER BY " + param.orderbyfieldname + " " + param.ascdesc + "" + offsetfilter + " ";


            return query;
        }
        public static string GetItemStockQuery(LogoQueryParam param)
        {
            var query = "SELECT \r\n" +
                        "ITEMS.LOGICALREF,\r\n" +
                        "ITEMS.CODE,\r\n" +


                        "(SELECT ISNULL((SUM (GNSTITOT.ONHAND)-SUM (GNSTITOT.RESERVED)),0) AS STOCKAMOUNT FROM " +
                               LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTST") +
                               " GNSTITOT WITH(NOLOCK) WHERE (GNSTITOT.STOCKREF = ITEMS.LOGICALREF) AND (GNSTITOT.INVENNO = -1)) AS GNTOTSTOCK," +

                        "(SELECT ISNULL((SUM (GNSTITOT.ONHAND)-SUM (GNSTITOT.RESERVED)),0) AS STOCKAMOUNT FROM " +
                               LogoUtils.TableViewNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "GNTOTST") +
                               " GNSTITOT WITH(NOLOCK) WHERE (GNSTITOT.STOCKREF = ITEMS.LOGICALREF) AND (GNSTITOT.INVENNO IN (" + (string.IsNullOrEmpty(param.usstocknr) ? "-1" : param.usstocknr) + ")) ) AS USTOTSTOCK" +

                        " FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "ITEMS") + " AS ITEMS WITH(NOLOCK) \r\n" +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "UNITSETL") +
                        " AS UNITSETL WITH(NOLOCK) ON (UNITSETL.UNITSETREF=ITEMS.UNITSETREF)  \r\n" +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "MARK") +
                        " AS MARK WITH(NOLOCK) ON MARK.LOGICALREF = ITEMS.MARKREF " +
                        " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "SPECODES") + " AS SPE WITH(NOLOCK) ON ITEMS.SPECODE = SPE.SPECODE AND SPE.CODETYPE = 1 AND SPE.SPECODETYPE = 14 " +
                        " WHERE (ITEMS.ACTIVE=0) AND (UNITSETL.MAINUNIT=1) AND (ITEMS.CARDTYPE NOT IN(4,20,21)) " + param.filter + "  \r\n" +
                        " ORDER BY " + param.orderbyfieldname + " " + param.ascdesc;


            return query;
        }
        public static string GetItemsCountQuery(LogoQueryParam param)
        {
            return "SELECT COUNT(ITEMS.LOGICALREF) FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "ITEMS") +
                   " AS ITEMS WITH (NOLOCK) WHERE (ITEMS.ACTIVE = 0) AND (ITEMS.CARDTYPE NOT IN(4,20,21)) " + param.filter;
        }
        public static string GetItemsPriceQuery(LogoQueryParam param)
        {
            return "SELECT " +
                    "'LF' AS kod, " +
                    "'Liste Fiyat' AS kod_baslik, " +
                    "'Liste Fiyat' AS aciklama, " +
                    "'0' AS tarih_aralik_durum, " +
                    "GETDATE() - 10 AS baslangic_tarihi, " +
                    "GETDATE() + 90 AS bitis_tarihi, " +
                    "1 AS durum, " +
                    "ITEMS.CODE AS urun_kodu, " +
                    "'TRY' AS doviz_kodu, " +
                    "CONVERT(DECIMAL(18, 4), PRCLIST.PRICE) AS liste_fiyati " +
                    "FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "PRCLIST") + " AS PRCLIST  WITH (NOLOCK) " +
                    " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "CLCARD") + " AS CLNTC WITH(NOLOCK) ON PRCLIST .CLIENTCODE = CLNTC.CODE " +
                    " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "PROJECT") + " AS PROJECT WITH(NOLOCK) ON PRCLIST .PROJECTREF = PROJECT.LOGICALREF " +
                    " LEFT OUTER JOIN " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "ITEMS") + " AS ITEMS WITH(NOLOCK) ON PRCLIST .CARDREF = ITEMS.LOGICALREF " +
                    " WHERE PRCLIST.PTYPE = 2 AND (PRCLIST.LOGICALREF IN (SELECT MAX(LOGICALREF) FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "PRCLIST") + " AS F GROUP BY CARDREF)) " + param.filter;
        }

        public static string GetOrderFicheNoQuery(LogoQueryParam param)
        {
            return "SELECT FICHENO FROM " + LogoUtils.TableNameWithFirmPlusPeriod(param.DbName, param.firmnr, param.periodnr, "ORFICHE") + " WHERE GENEXP4 = '" + param.datareference + "'";
        }
    }
}
