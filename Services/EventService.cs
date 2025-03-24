using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/events";

        // Mock data
        private readonly List<Event> _mockEvents = new()
        {
            new Event
            {
                Id = 1,
                Name = "Linköping water games",
                Description = "Linköpings Allmänna Simsällskap är stolta att bjuda in till det andra årliga Linköping Water Games 2025.\n\nVälkommen att delta i tre dagar av intensiva lopp, spännande avslutningar och rekordstora prestationer, i både simning och simhopp.\n\nMissa inte chansen att se några av de bästa simmare och dykare på plats, både svensk och internationell elit. Markera datumet i din kalender och gör dig redo för ett evenemang du sent kommer att glömma!",
                Start = new DateTime(2025, 3, 28),
                End = new DateTime(2025, 3, 30),
                Location = "Tinnerbäcksbadet",
                Price = 100,
                AvailableTickets = 500,
                MaxAttendees = 1000,
                Category = "sport",
                ImageUrl = "images/watergames.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 2,
                Name = "Ghost – Skeletour World Tour 2025",
                Description = "Ghost, ett av världens mest älskade band, åker ut på världsturné. Med över 55 datum i Europa, Nordamerika och Sydamerika nästa år så blir turnén den största hittills för det prisbelönta bandet. Under turnén blir det två konserter i Sverige, varav en i hemstaden Linköping 22 maj på Saab Arena.\n\nAs far as live acts go, they’re one of the best in the business” —THE LOS ANGELES TIMES\n\nVärldsturnén blir Ghosts första sedan RE-IMPERATOUR 2023, turnén som förevigades i bandets hyllade konsertfilm RITE HERE RITE NOW – och nu är det äntligen dags för turné igen. Turnén, bestående av 55(!) konserter, kommer att starta den 15 april i Manchester för att sen avslutas i september i Mexico City. Från dess att bandet grundades i Linköping för över 15 år sedan har de vuxit till att bli ett av Sveriges, och världens, mest älskade band. I Sverige såg vi Ghost senast våren 2022 där Aftonbladet bland annat skrev ”Saken är biff – Ghost är mer av en upplevelse än ett regelrätt rockband. Tobias Forge och hans namnlösa gastar har grepp om publiken i Avicii Arena redan från första sekund”.",
                Start = new DateTime(2025, 5, 22, 20, 0, 0),
                End = new DateTime(2025, 5, 22, 22, 30, 0),
                Location = "Saab Arena",
                Price = 855,
                AvailableTickets = 800,
                MaxAttendees = 3000,
                Category = "Music",
                ImageUrl = "images/ghost.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 3,
                Name = "We will rock you",
                Description = "WE WILL ROCK YOU är succémusikalen som baseras på låtar av Queen. Denna musikaliskt och visuellt storslagna föreställning åker nu ut på stor arenaturné efter sommarens stora succé på Dalhalla.\n\nVälkommen till en föreställning med högt tempo, spektakulär ljussättning, fantastisk kostymering och inte minst klassisk rockmusik av en ensemble som bland annat består av Sarah Dawn Finer, Viktor Norén, Dotter och Anton Ewald.\n\nMusikalen, som är skriven av den engelske komikern och författaren Ben Elton i samarbete med Queen-medlemmarna Brian May och Roger Taylor, utspelar sig i en dystopisk framtid där man undviker originalitet och individualism, och där rockmusik fallit i glömska. Men så dyker en ensam drömmare upp för att uppfylla en profetia som gör det möjligt för rocken att återkomma. Till viss del är musikalen också en satir över nöjesindustrin och Ben Elton säger att han därutöver hämtat viss inspiration från science fiction-thrillern Matrix.",
                Start = new DateTime(2025, 5, 23, 19, 30, 0),
                End = new DateTime(2025, 5, 23, 22, 0, 0),
                Location = "Saab Arena",
                Price = 695,
                AvailableTickets = 100,
                MaxAttendees = 2000,
                Category = "Music",
                ImageUrl = "images/rock.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 4,
                Name = "Saxon – Hell, Fire And Steel",
                Description = "Som del av sin Europaturné, “Hell, Fire And Steel” kommer det legendariska metalbandet Saxon till Linköping Konsert & Kongress 26 juli. Support är svenska metalbandet Tungsten.\n\nSaxon kommer inte bara framföra låtar från deras väl mottagna topp 20-album Hell, Fire And Damnation, utan också hela deras klassiska andra album Wheels of Steel (som släpptes för 45 år sedan). Dessutom kommer bandet att spela andra publikfavoriter och hits från hela karriären.\n\nBandets banbrytande album Wheels of Steel har blivit flitigt omnämnt av många journalister som ett av de klassiska album som varje metalfan borde äga. Fyllt med ikoniska låtar och tunga riff, från de inledande höghastighetsriffen i “Motorcycle Man”, levererar Wheels of Steel kraft från början till slut. Från det smittsamma och kanske största riffet i hitlåten “747 (Strangers in the Night)” till det mer avskalade, kraftfulla titelspåret “Wheels of Steel”. Detta är en unik möjlighet att se ett av metalvärldens bästa album spelas från början till slut i all sin storhet.\n\nSupport är det svenska metalbandet Tungsten. Trummisen Anders Johansson (ex Yngwie, Hammerfall, Manowar, Svullo, Hulkoff, Raubtier live, Owe Törnqvist mm) startade bandet 2016 tillsammans med sina söner Karl och Niklas och bandet fullbordades sedan med Mike Andersson som sångare. Deras debutalbum, “We will rise” släpptes i september 2019 och 2024 släpptes deras fjärde album The Grand Inferno.",
                Start = new DateTime(2025, 7, 26, 19, 00, 0),
                End = new DateTime(2025, 7, 26, 19, 00, 0),
                Location = "Linköping Konsert & Kongress",
                Price = 650,
                AvailableTickets = 100,
                MaxAttendees = 100,
                Category = "Music",
                ImageUrl = "images/saxon.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 5,
                Name = "Bolaget RODEO II",
                Description = "2 augusti stannar Bolagets turné RODEO II i Linköping som en av fem svenska städer. \r\nBolagets succéturné RODEO drog över 80 000 åskådare 2024 – hyllade RODEO bjöd på en maxad produktion och show som kulminerade i 24 000 fans som firade turnéavslutet på Stockholms Stadion den 14 september.\n\nNu står det klart att det kommer en uppföljare till succén. Sommaren 2025 drar RODEO II ut i landet för att landa i fem svenska städer – Malmö, Göteborg, Linköping, Falun och återigen avslutning på Stockholms Stadion.\n\nBolagets succé de senaste åren har nog inte undgått någon – med otaliga streamingrekord, dokumentärfilm och maxade spelningar som lockat storpublik. Det definitiva och slutliga beviset på hur stora de faktiskt är upplevde vi på Stockholms Stadion i september då hyllade turnén RODEO avslutades med fyrverkerier och 24.000 skrikande fans.\n\nEn stor del av framgången bakom RODEO 2024 grundade sig i en storytelling som fansen kunde ta del av under hela året, under evenemangen och i efterhand. Bolagets starka närvaro på sociala medier och en autentisk kontakt med sina fans gjorde att man kunde följa sagan i real time.",
                Start = new DateTime(2025, 8, 02, 17, 0, 0),
                End = new DateTime(2025, 8, 02, 20, 0, 0),
                Location = "Stångebrofältet",
                Price = 545,
                AvailableTickets = 200,
                MaxAttendees = 200,
                Category = "Music",
                ImageUrl = "images/rodeo.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 6,
                Name = "Diggiloo 2025",
                Description = "Succén är tillbaka! Diggiloo varvar upp motorerna inför sommaren 2025 med en superladdad samling stjärnor: Ola Salo, Sanna Nielsen, Medina, Uno Svenningsson, Mapei, Jessica Andersson, Arwin, Bruno Mitsogiannis, Lisa Stadell och en galet övertänd Per Andersson.\n\nHela 124.667 underbara människor kom och firade sommarens allra härligaste gemenskap på Diggiloo-turnén förra året. Vuxna, barn, mor-och farföräldrar njöt av picknickar och solnedgångar i Sveriges finaste parker, slott och torg. Tre generationer sjöng och dansade med varandra och alla underbara underhållare. 2025 längtar en ny fantastisk uppställning artister att få fira den svenska sommaren med dig och alla andra som en enda stor familj. Efter 21 år och miljontals besökare är Diggiloo verkligen en älskad tradition.\n\nVänta inte med att ordna biljetter till årets största konsert-show-upplevelse. Många orter blev slutsålda förra året, så för attvara säker på att inte behöva resa längre än nödvändigt är det bäst att vara snabb!",
                Start = new DateTime(2025, 8, 10, 18, 0, 0),
                End = new DateTime(2025, 8, 10, 21, 0, 0),
                Location = "Stångebrofältet",
                Price = 495,
                AvailableTickets = 500,
                MaxAttendees = 500,
                Category = "Music",
                ImageUrl = "images/diggiloo.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 7,
                Name = "Laleh sommarturné 2025",
                Description = "Laleh firar 20 år som artist och åker ut på storslagen sommarturné 2025! På turnén ska hon fira denna böljande musikaliska resa tillsammans med publiken och försöka förstå allt som hänt under dessa fullspäckade 20 år. Det blir en jubileumsturné med 17 stopp över hela Sverige.\n\nÄnda sedan genombrottet 2005 har den skygga Laleh varit noggrann med att musiken skulle tala för sig själv. Under dessa 20 år, och otaliga hits senare, har vi nu börjat lära känna denna unika artist lite mer och hennes plats i vår musikhistoria. Ingen är riktigt som Laleh, det finns bara en Laleh och hon har minst sagt gått sin egen väg.\n\nLaleh är musiker, sångare, låtskrivare och producent. År 2005 tog hon Skandinavien med storm med första singeln “Live Tomorrow”. Sedan dess har hon levererat klassiker som ”Some Die Young”, ”Colors” ”Bara Få Va Mig Själv” ”En stund på jorden” ”Goliat” och ”Det kommer bli bra”. Svensk viskonst blandas upp med modern popgenialitet – på både svenska och engelska har hon lyckats hitta ett eget språk, som talar direkt till vårt innersta, en ärlig poesi som vill sätta ord på det vi egentligen känner.",
                Start = new DateTime(2025, 8, 16, 20, 0, 0),
                End = new DateTime(2025, 8, 16, 23, 0, 0),
                Location = "Stångebrofältet",
                Price = 695,
                AvailableTickets = 1200,
                MaxAttendees = 5000,
                Category = "Music",
                ImageUrl = "images/laleh.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 8,
                Name = "Festival of the Midnight Sun 2025",
                Description = "Den 4-6 september 2025 återvänder Festival of the Midnight Sun till Skylten i Linköping!\n\nFestival of the Midnight Sun var namnet på Sveriges första stora musikfestival, 1970. Festivalen återuppstod 2019 genom ideella krafter från Linköping.\n\nLinköpings egna festival är tillbaka för ytterligare år med artister, mat, flipper. merch och lokalbryggd öl. Festivalen bygger vidare för att skapa en underbar helg, möten, upplevelser och kärlek till kulturen vi alla älskar!",
                Start = new DateTime(2025, 9, 4),
                End = new DateTime(2025, 9, 6),
                Location = "Skylten",
                Price = 780,
                AvailableTickets = 1200,
                MaxAttendees = 3000,
                Category = "Festival",
                ImageUrl = "images/midnight.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 9,
                Name = "Bonfire",
                Description = "Bonfire Festival gör ett glödhett återbesök till Linköping och Stångebrofältet den 5-6 september 2025. Med tre succéartade år i ryggen så ser vi ingen anledning till varför 2025 års upplaga inte kommer få hela Linköping att koka!\n\nLineup hittils: Veronica Maggio, Soppgirobygget (NO), Adaam & DJ Özz (after party)\r\nBonfire Festival är en glödhet musikfestival arrangerad av Homerun Festivals – skaparna av succéfestivalerna Brännbollsyran och Bayside Festival.",
                Start = new DateTime(2025, 9, 5),
                End = new DateTime(2025, 9, 6),
                Location = "Stångebrofältet",
                Price = 1899,
                AvailableTickets = 1200,
                MaxAttendees = 10000,
                Category = "Festival",
                ImageUrl = "images/bonfire.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 10,
                Name = "SALO & JÖBACK: A Show Extravaganza",
                Description = "Ola Salo och Peter Jöback är två artister som inte behöver någon närmare presentation, båda har enastående karriärer med ett brett artisteri som rört sig över flera genres. Allt från gigantiska hits, nationella anthems, The Ark, succémusikaler i Sverige, West End och Broadway, älskad julmusik till samarbeten med internationella popartister.\n\nMen vad händer vid en fusion – när dessa två musikaliska, extroverta, eleganta, flamboyanta entertainers gör en arenashow tillsammans? Vi vet. Det kommer att slå gnistor.\n\nPubliken kommer bjudas på genreöverskridande musik som representerar båda artisternas låtkataloger med sina otaliga hits, men även deras egna favoriter av andra artister, så räkna med stora nummer, magiska möten och en hel del överraskningar under 100 smockfyllda minuter tillsammans med ett stort tiomannaband samt dansare.\n\nHär har ni helt enkelt Sveriges största artister som möts i en show där även deras rivalitet och brödraskap, likheter och olikheter skapar både spänning och samspel.\r\nFörvänta er ett enda stort scenfyrverkeri i mötet mellan dessa två konstnärer, med sin mångåriga scenerfarenhet. Tillsammans är de något mycket större än vi kunnat ana. Det blir A Show Extravaganza.",
                Start = new DateTime(2025, 10, 11, 19, 30, 0),
                End = new DateTime(2025, 10, 11, 22, 30, 0),
                Location = "Saab Arena",
                Price = 650,
                AvailableTickets = 1200,
                MaxAttendees = 3000,
                Category = "Music",
                ImageUrl = "images/salo.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            },
            new Event
            {
                Id = 11,
                Name = "A Show Larger Than Life",
                Description = "Efter två succésäsonger i Stockholm och Göteborg åker publik- och kritikerhyllade \"A Show Larger Than Life\" ut på en arenaturné hösten 2025.\n\nShowen hyllar boyband-erans allra största hits och de svenska upphovspersoner som erövrat en hel värld med sina låtar. Upplev en episk musik-era, från tidigt 60-tal fram till idag, med världskända hits från bland andra Backstreet Boys, New Kids on The Block, Westlife, Take That, *NSYNC, One Direction, Jackson Five och många fler.\n\nDet blir festernas fest och helt omöjligt att sitta still när tiomannaensemblen med några av landets främsta liveartister, dansare och musiker, bland andra David Lindgren, Boris René, Greg Curtis, Erik Segerstedt och Mattias Andréasson drar igång showen.\n\nBakom flera av hitsen i showen står kända svenska producenter och låtskrivare som Max Martin, Jörgen Elofsson, Andreas Carlsson, Denniz Pop och Lisa Miskovsky, vars mästerverk nu levereras i ett helt nytt, oemotståndligt sound. Allt under ledning av kapellmästare Kristian Kraftling, känd från bl a från Dirty Loops. A Show Larger Than Life är en nostalgisk lyckokick framförd med storslagen finess och live-eufori, dedikerad ALLA partysugna musikälskare!",
                Start = new DateTime(2025, 11, 21, 19, 30, 0),
                End = new DateTime(2025, 11, 21, 22, 30, 0),
                Location = "Saab Arena",
                Price = 495,
                AvailableTickets = 1200,
                MaxAttendees = 3000,
                Category = "Music",
                ImageUrl = "images/ashow.png",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            }
            
        };

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            try
            {
                // Actual API call (commented out)
                // return await _httpClient.GetFromJsonAsync<List<Event>>(_baseUrl) ?? new List<Event>();

                // Mock data
                return _mockEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting events: {ex.Message}");
                return new List<Event>();
            }
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            try
            {
                // Actual API call (commented out)
                // return await _httpClient.GetFromJsonAsync<Event>($"{_baseUrl}/{id}");

                // Mock data
                return _mockEvents.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting event by id: {ex.Message}");
                return null;
            }
        }

        public async Task<Event> CreateEventAsync(Event evt)
        {
            try
            {
                // Using mock data
                evt.Id = _mockEvents.Max(e => e.Id) + 1;
                evt.CreatedAt = DateTime.Now;
                evt.UpdatedAt = DateTime.Now;
                _mockEvents.Add(evt);
                return evt;
                
                // Actual API call (commented out)
                // var response = await _httpClient.PostAsJsonAsync(_baseUrl, evt);
                // response.EnsureSuccessStatusCode();
                // return await response.Content.ReadFromJsonAsync<Event>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating event: {ex.Message}");
                return null;
            }
        }

        public async Task<Event> UpdateEventAsync(Event evt)
        {
            try
            {
                // Using mock data
                var existingEvent = _mockEvents.FirstOrDefault(e => e.Id == evt.Id);
                if (existingEvent != null)
                {
                    existingEvent.Name = evt.Name;
                    existingEvent.Description = evt.Description;
                    existingEvent.Start = evt.Start;
                    existingEvent.End = evt.End;
                    existingEvent.Location = evt.Location;
                    existingEvent.Price = evt.Price;
                    existingEvent.AvailableTickets = evt.AvailableTickets;
                    existingEvent.MaxAttendees = evt.MaxAttendees;
                    existingEvent.ImageUrl = evt.ImageUrl;
                    existingEvent.Category = evt.Category;
                    existingEvent.IsActive = evt.IsActive;
                    existingEvent.UpdatedAt = DateTime.Now;
                }
                return existingEvent;
                
                // Actual API call (commented out)
                // var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{evt.Id}", evt);
                // response.EnsureSuccessStatusCode();
                // return await response.Content.ReadFromJsonAsync<Event>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating event: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            try
            {
                // Using mock data
                var eventToDelete = _mockEvents.FirstOrDefault(e => e.Id == id);
                if (eventToDelete != null)
                {
                    _mockEvents.Remove(eventToDelete);
                }
                
                // Actual API call (commented out)
                // var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                // response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting event: {ex.Message}");
            }
        }

        public async Task<List<Event>> SearchEventsAsync(string searchTerm)
        {
            try
            {
                // Using mock data
                return _mockEvents.Where(e =>
                    e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    e.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    e.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                
                // Actual API call (commented out)
                // return await _httpClient.GetFromJsonAsync<List<Event>>($"{_baseUrl}/search?term={Uri.EscapeDataString(searchTerm)}") 
                //     ?? new List<Event>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching events: {ex.Message}");
                return new List<Event>();
            }
        }
    }
} 