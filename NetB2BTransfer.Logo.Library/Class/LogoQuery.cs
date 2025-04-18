using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Logo.Library.Class
{
    public class LogoQuery
    {
        public static string GetArpsQuery(LogoQueryParam param)
        {
            var offsetfilter = "";
            if (param.limit != "-1")
            {
                offsetfilter = " OFFSET " + param.offset + " ROWS \r\n" +
                               " FETCH NEXT " + param.limit + " ROWS ONLY;";
            }

            var query = "SELECT\r\n" +
                        "(SELECT COUNT(CLCARD.LOGICALREF) FROM " + LogoUtils.TableNameWithFirm(param.DbName, param.firmnr, "CLCARD") +
                        " CLCARD WITH (NOLOCK) WHERE (CLCARD.ACTIVE = 0) AND (CLCARD.CARDTYPE<>22) " + param.filter + ") AS TOTALCOUNT,\r\n" +
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
    }
}
