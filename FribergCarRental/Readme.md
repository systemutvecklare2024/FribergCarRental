# Friberg Car Rental

## Install
- Update the field ``ConnectionStrings`` in ``appsettings.json``
- Database setup inside ``Package Manager Console``
	- ``Add-Migration Initial``
	- ``Update-Database``

## Användarkrav –User Stories
Följande user stories har tagits fram och tydliggör vad kunder ska kunna göra 
och vad administratören ska kunna göra.

### Som kund vill jag kunna
- [x] lista alla bilar som finns att hyra
- [x] se en eller flera bilder för bilen jag funderar på att hyra
- [x] skapa en bokning för en viss bil med ett startdatum och ett slutdatum
- [x] få en bekräftelse på bokningen
- [x] se mina tidigare bokningar
- [x] ta bort en kommande bokning.

### Som administratör vill jag kunna
- [x] Lista, lägga till, ändra och ta bort bilar
- [x] lista, lägga till, ändra och ta bort kunder
- [x] lista och ta bort beställningar.