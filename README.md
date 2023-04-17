# IbkrToSru

![github actions](https://github.com/JohanLarsson/IbkrToSru/actions/workflows/ci.yml/badge.svg)

App for generating .sru files from IBKR csv

1. Exportera activity report csv från [IBKR](https://www.interactivebrokers.co.uk/sso/Login?SERVICE=AM.LOGIN) Performance & Reports > Statements > Activity
2. Läs in csv genom att klicka på ...
3. Ange år
4. Ange genomsnittlig växelkurs för året från riksbanken https://www.riksbank.se/sv/statistik/sok-rantor--valutakurser/valutakurser-till-deklarationen/
5. Ange personnummer
6. Spara som BLANKETTER.sru
7. Importera BLANKETTER.sru i [mina sidor](https://www.skatteverket.se/)
8. Kontrollera att uppgifterna stämmer

![image](https://user-images.githubusercontent.com/1640096/232188438-b461b158-8187-472b-b8f1-462429120f7d.png)

Endast stöd för handel i USD som det är nu.

![image](https://user-images.githubusercontent.com/1640096/232244461-4c3233bc-1acb-493d-94e2-9e6369cb65cb.png)
