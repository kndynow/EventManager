# TicketToCode Challenge

Ett Event Management System

## Översikt

I detta grupprojekt ska ni skapa ett komplett Event Management-system med Blazor, WebAPI och Entity Framework Core. Systemet ska låta användare bläddra bland, boka och hantera evenemang, samt innehålla administrativa funktioner för att skapa och hantera evenemangen. Ni får även stöd för inloggning från en färdig backend-lösning, medan ni själva implementerar den i er Blazor-klient.

## Teknisk specifikation

- **Frontend:** Blazor
- **Backend:** ASP.NET Core WebAPI (inkl. förenklad inloggning/auth)
- **Dataaccess:** Entity Framework Core (valfritt)
- **Versionshantering:** Git (ni forkar ett GitHub-repo och kan sedan synka uppdateringar)

## Funktionella krav (Kravspecifikation)

### För användare

- Bläddra/söka bland kommande evenemang
- Visa detaljerad information om evenemang
- **Förenklad inloggning** (ni använder de metoder som redan finns i API:t)
- Boka biljetter till evenemang
- Se sina bokningar/biljetter
- Avboka biljetter

### För administratörer

- Skapa nya evenemang
- Redigera/ta bort befintliga evenemang
- Se bokningar för ett evenemang
- Hantera användare (ex. redigera, ta bort)
- Se enkel statistik över bokningar/evenemang

## Tekniska krav

1. **Git-användning:**

   - Forka det givna projektet på GitHub
   - Använd feature branches för ny funktionalitet
   - Implementera pull requests för kodgranskning
   - Dra in uppdateringar (upstream changes) från lärarens repo vid behov

2. **Kod och struktur:**

   - Tydlig separation mellan frontend (Blazor) och backend (API)
   - Förenklad inloggning via API:t (ni fokuserar på att anropa metoder och lagra token/cookie)
   - Validering av användarinput
   - Dokumentation av kod och API
   - Meningsfulla variabel- och funktionsnamn

3. **Scrum-liknande arbetssätt med dagliga standups:**

   - Inför dagliga möten kl 09:00 i Teams
   - Ni delas in i breakout rooms för korta standups (ca 5–10 min)
   - Jag (lärare) hoppar runt mellan rummen för att följa upp läget
   - **Roterande Scrum Master** – låt olika personer i gruppen axla rollen för att leda standups, följa upp tasks och se till att ni följer planen

4. **Utvecklingsprocess:**
   - Planera arbetet genom att bryta ned kraven i mindre uppgifter
   - Fördela arbetet inom gruppen och rotera ansvarsroller
   - Utför kodgranskningar och använd tex. GitHub PR-flöden
   - Dokumentera viktiga designbeslut

Oavsett vilket alternativ ni väljer ska ni:

- Ha minst tre huvudsakliga datamodeller och relationer
- **(viktigt)** Ta fram en kravspecifikation för er projekt
- Säkerställa fungerande inloggning (från färdig backend) (valfritt)
- Implementera enklare affärslogik (ej bara datalagring)
- Dokumentera koden och använda Git flitigt

## Bedömning

Projektet bedöms på följande kriterier:

- Funktionalitet och användarupplevelse
- Kodkvalitet och struktur
- Git-användning och samarbete
- Dokumentation (Kod, design, standups)
- Prestation vid kodgenomgång

## Inlämning och redovisning

- Komplett källkod via GitHub-fork
- Kort presentation av projektet

### Inlämning

Projektet lämnas in som en länk till ett github-repo, som ni har forkat från mitt repo. Projektet ska vara inlämnat vid presentationen, så att jag kan se er projekt bättre.

### Redovisning

Vi kommer utföra redovisningen i form av en individuell presentation av projektet, detta görs i tvärgrupper. Dvs. vi bryter ut 3st grupper, med en student från varje grupp, och redovisar i dessa grupper. Detta dels för att jag ska kunna bedömma er individella förtåelse av projektet, och dels för att höra mer om er arbetsprocess, men framförallt för att ni ska få en chans att visa upp ert projekt för varandra.

Redovisningarna kommer delas upp i tre tillfällen; en på fördmiddagen och en på eftermeddagen torsdag den 27/03/2025, samt förmiddag fredag den 28/03/2025.

Upplägg för redovisningen bör vara följande:

- Kort presentation av projektet (ca 10-15 minuter)
  - Demo av funktionaliteten (gärna med en testanvändare)
  - Kodgenomgång där ni förklarar era designval
  - Diskussion om er arbetsprocess
  - Svar på frågor

## Viktiga datum

- **Projektstart:** 11/03/2025
- **Eventuell egen kravspecifikation:** 14/03/2025
- **Presentationer:** 27/03/2025, 28/03/2025

**Lycka till med ert projekt!**
