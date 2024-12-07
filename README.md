Slutuppgift: Bibliotekssystem

Bakgrund
Du ska bygga en databas för ett bibliotek där böcker och författare kan hanteras.
Målet är att implementera en databaslösning som stödjer funktioner för att hantera böcker, författare och boklån.
Uppgiften testar dina kunskaper i databasdesign, relationshantering och Entity Framework.

Uppgift
- Du ska skapa ett bibliotekssystem som uppfyller följande krav:

Del 1: Databasdesign
- ER-modell och ER-diagram

Identifiera och beskriv vilka entiteter, attribut, och relationer som behövs för systemet.
Rita ett ER-diagram som visar entiteter, relationer och attribut.
Tydliggör att:
En bok kan ha flera författare.
En författare kan ha skrivit flera böcker (många-till-många-relation).
Entiteter och relationer

Bygg en databas som inkluderar följande:
En tabell för böcker.
En tabell för författare.
En bryggtabell för att hantera många-till-många-relationen mellan böcker och författare.
En tabell för att hantera utlåning av böcker.

--------------------------------------------------------------

Del 2: Implementering i Entity Framework
- Modellklasser

Implementera entiteter och relationer i Entity Framework.
Skapa en kontextklass som kopplar modellen till en databas.
CRUD-operationer

Implementera funktioner för att:
Skapa en ny författare.
Skapa en ny bok.
Lägga till en relation mellan en bok och en författare.
Lägga till ett lån för en bok.
Ta bort författare, böcker och lån.
Visa data

Skapa funktioner för att:
Lista alla böcker tillsammans med deras författare.
Lista alla lånade böcker och deras återlämningsdatum.
Seed-data

Fyll databasen med grunddata som automatiskt läggs till när systemet körs.

-------------------------------------------------------------------

Del 3: Fördjupning (valfritt för Väl Godkänt)
- Visa relationer

Implementera funktioner för att:
Lista alla böcker som en specifik författare har skrivit.
Lista alla författare som har bidragit till en viss bok.
Lånehistorik

Implementera en funktion för att visa en historik över lånade böcker.

-------------------------------------------------------------

Inlämning
Lämna in en länk till din lösning på GitHub i Learnpoint senast kl 23:00 den 7/12.
Alla kodändringar gjorda efter det klockslaget kommer att ignoreras.
OBS! Se till att dina migrations är inkluderade i Git. Jag vill också se att ni har gjort mer än en commit så att jag kan följa er process.

---------------------------------------------------------------

Om man vill göra mer
Det är fritt att utöka databasen, så länge grundfunktionerna finns kvar. Kom dock ihåg att ju fler saker man lägger till, desto fler saker kan gå sönder.

-----------------------------------------------------------------

Bedömningskriterier
Godkänt:

En korrekt databasmodell som inkluderar tabellerna för böcker, författare, relationer och lån.
Grundläggande CRUD-operationer fungerar.
Seed-data är implementerat.
Ett tydligt ER-diagram har lämnats in.
Väl Godkänt:

Funktioner som lånehistorik och visning av relationer mellan böcker och författare är implementerade.
Projektet är välstrukturerat och följer god kodstandard.
Inkluderar detaljerad dokumentation.

------------------------------------------------------------------

Tips
Börja med att planera din databasdesign innan du skriver någon kod.
Se till att förstå hur relationerna mellan tabellerna fungerar.
Testa din kod regelbundet för att undvika att bygga upp för många problem samtidigt.
Kontakta mig om du har några frågor eller behöver hjälp.
