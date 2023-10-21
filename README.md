# FTSH API Client
Konzolos alkalmazás REST API tesztelésére

## Telepítés
#### Windows rendszeren
- Az `ms_installer` mappában található setup.exe elindításával, megjelenik a ClickOnce telepítő.
![image](https://github.com/varsanyib/apiclient/assets/109478771/0ff34c01-1d54-401d-9911-504707202165)

- Az Install gombra kattintva feltelepül, majd elindul az alkalmazás.

#### Linux és Mac OS rendszereken
- A `portable` mappában található dll fájlok és a `dotnet` keretrendszer telepítésével futtatható az állomány.

## Működése
- Az alkalmazás elindítása után, a főmenü jelenik meg, ahol 9 menüpont található.
- 2-6. menüpontokig a lekérdezések hajthatóak végre, az 1. és 7-9. az alkalmazás vezérléséhez használható.
![image](https://github.com/varsanyib/apiclient/assets/109478771/9777a1f0-f202-4571-b774-b1d76e21267e)

##### 1. Szerver elérési útvonalának beállítása
- Lehetőség van a távoli interfész elérhetőségének megadására, így a lekérdezésekben nincs szükség a megadására, viszont a hivatkozás kiegészítésére mindig van lehetőség.
![image](https://github.com/varsanyib/apiclient/assets/109478771/608e48cf-7a87-4b3f-bc44-6bb5b0f41b5f)

##### 2. GET
- A menüpontba való belépés után, megadható egy megnevezés, amely a visszakeresésnél lehet a felhasználó segítségére.
- A megnevezés után megadható a hivatkozás kiegészítése. (A fix elérési útvonal ilyenkor már automatikusan megjelenítésre kerül!)
![image](https://github.com/varsanyib/apiclient/assets/109478771/c553f890-962d-4f75-a6d5-68bd2072da48)
- Az enter billentyű lenyomása után lefut a lekérdezés, majd kilistázásra kerülnek a technikai információk.
- Minden lekérdezés egy egyedi azonosítóval kerül - a szoftver futásáig - eltárolva.
![image](https://github.com/varsanyib/apiclient/assets/109478771/c0c9b131-6169-4847-881e-3245804434dc)

##### 3. POST - 4. PUT - 5. PATCH - 6. DELETE
- A menüpontba való belépés után, az első két kérdés megegyezik a GET kéréssel.
- A harmadik kérdés a kulcs-érték párok számára kérdez rá, ennek megadása után, egyesével van lehetőség felvinni a párokat.
![image](https://github.com/varsanyib/apiclient/assets/109478771/091c1463-0978-48b0-af25-8e7c0e873356)

![image](https://github.com/varsanyib/apiclient/assets/109478771/f32c3293-5a8c-4dde-bd50-341a836c3814)

##### 7. Tárolt lekérdezések megtekintése
- A szoftver folyamatosan tárolja a következő bezárásig a lekérdezéseket, ezek bármikor visszakérhetőek.
![image](https://github.com/varsanyib/apiclient/assets/109478771/373d4e9b-9e62-4f23-a303-f819e65e0da3)
- A lekérdezés neve mellett található azonosító beírására, az előzőleg lekért adatok tekinthetőek meg újra. (A lekérdezés nem fut le újra!)
![image](https://github.com/varsanyib/apiclient/assets/109478771/546bcaef-0f66-44e7-86d0-c88dc1ca109b)

##### 8. Tárolt lekérdezések törlése
- Az alkalmazás futtatása közben is van lehetőség, az eltárolt lekérdezések törlésére, amely a menüpontba belelépve az i/I billentyű bevitelére végrehajtható.
![image](https://github.com/varsanyib/apiclient/assets/109478771/72e4fd5a-ade9-4993-9237-5178f43471e0)

##### 9. Kilépés
- Bezárja az alkalmazást.

## DLL
- Az APIReq osztály tartalmazza a lekérdezések kezelését, az APIAnswer osztály bevonásával történik a kiértékelés és adatok kihasználhatósága.
