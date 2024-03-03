# IbkrToSru

[![ci](https://github.com/JohanLarsson/IbkrToSru/actions/workflows/ci.yml/badge.svg)](https://github.com/JohanLarsson/IbkrToSru/actions/workflows/ci.yml)

App för att generera .sru-filer frpn IBKR csv. Sru filerna kan sen importeras i skatteverkets mina sidor.

0. Om du är programmerare bygger du från kod, annars kan du ladda hem .exe [här](https://github.com/JohanLarsson/IbkrToSru/releases)
1. Exportera activity report csv från [IBKR](https://www.interactivebrokers.co.uk/sso/Login?SERVICE=AM.LOGIN) Performance & Reports > Statements > Activity
2. Läs in csv genom att klicka på ...
3. Ange år
4. Ange genomsnittlig växelkurs för året från riksbanken https://www.riksbank.se/sv/statistik/sok-rantor--valutakurser/valutakurser-till-deklarationen/
5. Ange personnummer
6. Spara som BLANKETTER.sru
7.a Importera BLANKETTER.sru i [mina sidor](https://www.skatteverket.se/)
7.b Om du har fler än 300 affärer laddar du upp BLANKETTER.sru och INFO.sru via filöverföringstjänsten
8. Kontrollera att uppgifterna stämmer

![image](https://github.com/JohanLarsson/IbkrToSru/assets/1640096/2d396d33-a5bc-4b95-ab5b-7e7da5b80e2c)

Endast stöd för handel i USD som det är nu.

## IBKR Activity report

För att ladda hem .csv-filen som används av IbkrToSru
1. Loggar in på [ditt konto](https://www.interactivebrokers.co.uk/en/home.php)
2. Klicka `Performance & Report`
3. KLicka på `Statements`
4. Ladda hem csv för aktuellt år genom att klicka på den blå pilen till höger om `Activity`

![image](https://github.com/JohanLarsson/IbkrToSru/assets/1640096/d090b188-b8ed-47c4-9b96-3d02647d503e)

![image](https://user-images.githubusercontent.com/1640096/232244461-4c3233bc-1acb-493d-94e2-9e6369cb65cb.png)

## INFO.sru

```
#DATABESKRIVNING_START
#PRODUKT SRU
#FILNAMN BLANKETTER.SRU
#DATABESKRIVNING_SLUT
#MEDIELEV_START
#ORGNR 198011128072
#NAMN Johan Larsson
#ADRESS Gatan 1
#POSTNR 12345
#POSTORT Staden
#EMAIL johan@hotmale.com
#MEDIELEV_SLUT
```
