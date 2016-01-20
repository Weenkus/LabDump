Projekt je napisan u Ruby on Rails.
Za pokrenuti projekt treba instalirati rails okruženje, u Linuxu
to je dohvat railsa sa bilo kojim packet manager.
Uz rails treba instalirati i mongodb, također preko proizvoljnog packetManager-

Bitno je prije pokretanje rails servera instalirati sve potrebne ruby gemove potrebne
za rad aplikacije, to se može sa komandom "bundle install" iz root foldera u projektu.

Nakon instalacije Rails okruženja, sa terminalom (Linux) pozicionirati se u root folder
projekta (portal) i napisati "rails s", naredba s kojom se pokreće rails server.
Defaultna localhost adresa je 3000, ali za svaki slućaj pogledaj koja se adresa ispiše
nakon pokretanje rails servera.

Nakon odlaska na adresu danu sa strane rails servera (npr. localhost:3000) vidi se webSite.
Svi podaci testni podaci se mogu unjeti preko same web aplikacije.

Ime baze je portal_development i može se pristupiti iz terminala sa naredbom "mongo portal_development"

KUHARICA:
1. Instalirati Ruby, Rails, MongoDB
2. Pozicionirati se u root folder projekta (portal)
3. Runati komandu "bundle install"
4. Pokrenuti rails server sa komandom "rails s"
5. Otići na localhost koji je javio rails server (najčešće localhost:3000)
6. Preko websitea dodavati testne primjere

Ako nešto ne radi slobodno kontaktirajte me na vinko.kodzoman@fer.hr