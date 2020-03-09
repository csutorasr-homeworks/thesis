# Dimplomaterv

[Architektúrális leírás](architecture.uxf) (Megnyitható UMLet-tel.)

## Specifikáció

A cél egy olyan SAAS alkalmazás készítése, ami cégek számára karbantartható módon tudja kezelni a vállalatok autóflottáját.

Ehhez tartozik az flották autóinak számon tartása, valamint azok tulajdonosának. Az üzemanyag fogyasztás és a megtett kilométerek számolása a fő cél. Ennek a folyamatnak a gördülékeny, intuitív megoldása.

A cégvezetők számára a havi kerettúllépések, és azoknak a számontartása szükséges funkció. Ezt átláthatóan és egyszerűen kell tudniuk megtenni a lehető legkevesebb időpazarlással.

### Use-case-k

1. Bejelentkezés
1. Kijelentkezés
1. Regisztráció
1. Flották megtekintése
1. Flotta létrehozása
1. Flotta módosítása
1. Flotta törlése
1. Flottakezelők módosítása
1. Autók megtekintése
1. Autó létrehozása
1. Autó módosítása
1. Autó deaktiválása
1. Autóhoz tartozó bejegyzések megtekintése
1. Bejegyzés létrehozása
1. Bejegyzés töröltre állítása
1. Összegzés megtekintése
1. Összegzés jóváhagyása
1. Befizetések listázása
1. Befizetés jóváhagyása
1. Szervíz figyelmeztetések megtekintése
1. Szervíz feltételek létrehozása
1. Szervíz feltételek törlése
1. Szervízalkalmak megtekintése
1. Szervízalkalmak létrehozása
1. Szervízalkalmak törlése

## Nem funkcionális követelmények

1. Az alkalmazás használható legyen az örökzöld webböngészőkből.
1. A folyamatok a lehető legegyszerűbbek legyenek.
1. Az üzleti logikai szabályokat rendszerezve szervezzük.
1. Mobilnézet támogatása.
1. Authentikáció bővíthetősége.
1. Robosztus adatbázis kezelés.
1. Biztonságos, OWASP-nak megfelelő megvalósítások.
1. Tesztelhetőség.

## Interfészek

Az architektúrális leírás interfészeinek részletezése.

### Authentikáció

Az authentikációt két interfész írja le. Egyik az Identity Server felé, másik a további providerek felé az OAuth.

### Adatbázis

Az adatbázis felé a MongoDB saját Wire Protocollján keresztül kommunikálunk.

### Alkalmazás

Az alkalmazás API-ját különböző módszerekkel ajánljuk ki. Ez lehet WebApi vagy WebSocket, ami a Blazorrel kompatibilis. Ezt az alkalmazás fejlesztése során hozott döntésektől függ.

## Entitások

### Felhasználó

Egy bejelentkezett felhasználót azonosít. A belépés módját meghatározza, és ezeket össze lehet kötni a segítségével.

- azonosító
- felhasználónév
- e-mail cím
- jelszó

### Flotta

Az egység, ami összefogja a felhasználók autóit és az ehhez kapott jogosultságait.

- azonosító
- név
- hozzá tartozó autók
- flottakezelők

### Autó

A felhasználók birtokolják az autókat. Minden autó egy céghez tartozik.

- rendszám
- havi költhető keret

### Bejegyzés

Ezek az egységei a méréseknek. Minden mérés egy autóhoz tartozik. Minden tankolás esetén készül ilyen bejegyzés. Ezekkel lehet dologozni és mérni később a feladat során.

- km óra állása
- időpont
- helyszín
- mennyiség
- összeg

### Összegzés

Ez egy aggregált adat. A gyorsabb adatelérés érdekében aggregálva is gyűjtük a historikus adatokat. Előfizetés nélkül egy hónapig tartja meg a szolgáltatás a bejegyzéseket.

- havi összes összeg
- havi keret
- megtett km-ek
- jóváhagyva

### Befizetés

A keretet túllépve meg kell fizetni a túlköltött összeget. Ezt a program automatikusan méri. Befizetést hoz létre a hónap végén, amit minden jóváhagyott összesítőből készít.

- összeg
- befizetve

### Szervíz

A szervíz egy autóhoz köthető időpont, amikor az autó szervízben járt.

- km óra állás
- időpont
- szervíz típusa (Javítás, karbantartás, kötelező szervíz)

### Szervíz időszabály

Egy adott szabály, ami egy adott idő megteltével jelez, hogy az autót szervízbe kell vinni. Autóhoz köthető.

- gyakoriság

### Szervíz km szabály

Egy adott szabály, ami egy adott km megteltele után jelez, hogy az autót szervízbe kell vinni. Autóhoz köthető.

- km

## Alkalmazáson belüli kommunikáció

Az alkalmazás a frontenddel fog kommunikálni. Ehhez szükségesek a kommunikáció alapját adó DTO-k.

### GET /flottak

Minden flottát visszaad.

### POST /flottak

Új flottát hoz létre.

- név

### PUT /flottak/{id}

Flotta módosítása.

- név

### DELETE /flottak/{id}

Flotta törlése.

### POST /flottak/{id}/kezelok

Kezelő felvétele a flottába.

- kezelőazonosító

### DELETE /flottak/{id}/kezelok/{id}

Kezelő törlése a flottából. Ha ő lenne az utolsó, akkor nem lehet.

- kezelőazonosító

### GET /flottak/{id}/autok

Flotta autóinak listázása.

- rendszám
- oldalszám
- oldalhossz

### POST /flottak/{id}/autok

Autót hoz létre.

- rendszám
- havi keret

### PUT /flottak/{id}/autok/{id}

Autót módosít.

- rendszám
- havi keret

### DELETE /flottak/{id}/autok/{id}

Autó törölt jelzése.

- oldalszám
- oldalhossz

### GET /flottak/{id}/autok/{id}/bejegyzesek

Bejegyzéseit listázza egy autónak.

### POST /flottak/{id}/autok/{id}/bejegyzesek

Bejegyzést hoz létre.

- km óra állása
- időpont
- helyszín
- mennyiség
- összeg

### DELETE /flottak/{id}/autok/{id}/bejegyzesek/{id}

Bejegyzés törölt jelzése.

### GET /flottak/{id}/autok/{id}/osszegzesek

Autónak a havi összesítőit kéri le.

- oldalszám
- oldalhossz

### POST /flottak/{id}/autok/{id}/osszegzesek/{id}/jovahagy

Jóváhagyja a havi összesítőt.

### POST /flottak/{id}/autok/{id}/osszegzesek/{id}/elutasit

Elutasítja a havi összesítőt.

### GET /flottak/{id}/autok/{id}/befizetesek

Autónak a befizetéseit kéri le

- befizetett

### POST /flottak/{id}/autok/{id}/befizetesek/{id}/jovahagy

Jóváhagyja a befizetés teljesülését.

### GET /flottak/{id}/autok?szervizKell=1

Lekéri azokat az autókat, amiknek szervízre van szüksége.

### GET /flottak/{id}/autok/{id}/szervizfeltetelek

Az autó szervízfeltételeit mutatja.

### POST /flottak/{id}/autok/{id}/szervizfeltetelek/ido

Új idő alapú szervízfeltételt hoz létre.

- időintervallum

### POST /flottak/{id}/autok/{id}/szervizfeltetelek/km

Új km alapú szervízfeltételt hoz létre.

- km

### DELETE /flottak/{id}/autok/{id}/szervizfeltetelek/{id}

Töröl egy szervízfeltételt.

### GET /flottak/{id}/autok/{id}/szervizek

Lekéri egy autó szervízelési időpontjait.

- oldalszám
- oldalhossz

### POST /flottak/{id}/autok/{id}/szervizek

Létrehoz egy szervízalkalmat.

### DELETE /flottak/{id}/autok/{id}/szervizek/{id}

Töröltre állít egy szervízalkalmat.

## Felület tervek

A felületek többnyire egyszerű listázó felületek. Ezeket egyszerű drótvázakkal szemlétetem. Link elérhető [itt](https://wireframepro.mockflow.com/view/Mb5ea37bb03fcaaf4765351cb97267b0b1583797216463).

A létrehozó felületen minden adatot meg kell adni, ami szükséges.
