from time import time
import numpy as np
import matplotlib.pyplot as plt

class Tomografia:
    
    def __init__(self):
        self.prostokaty = ([
            [1, [-0.4,-0.2],[-0.5,0.5]],
            [2, [-0.2,0.2],[0.3,0.5]],
            [3, [-0.2,0.2],[-0.1,0.1]],
            [4, [0,0.2],[0.1,0.3]],
            [5, [0.6,0.8],[-0.8,-0.6]]
        ])
        self.ilosc_zrodel = 40
        self.ilosc_pixeli_na_bok = 40
        self.wygenerowane_pixele_kwadraty = []
        #self.odczytaj_dane()
        
    
    def wygeneruj_pixele_kwadraty(self):
        
        #kazdy wpis do tablicy to odpowiednio:
        #zakres x i zakres y kwadrata na którego podstawie będziemy liczyć przecięcia
        
        dystans_miedzy_pixelami = 2/self.ilosc_pixeli_na_bok
        zbior_x = [(-1)+x*dystans_miedzy_pixelami for x in range(0,self.ilosc_pixeli_na_bok+1)]
        #zbior_y = [(1)-x*dystans_miedzy_pixelami for x in range(0,self.ilosc_pixeli_na_bok+1)]
        
        for i_x in range(0,len(zbior_x)-1):
            for i_y in range(0,len(zbior_x)-1):
                self.wygenerowane_pixele_kwadraty.append(
                    [(zbior_x[i_x],zbior_x[i_x+1]),(zbior_x[i_y],zbior_x[i_y+1])]
                )
        #print(self.wygenerowane_pixele_kwadraty)

    
    def odczytaj_dane(self):
        ile_prostokatow = int(input(
            "Witaj w programie\nProszę o podanie ilości prostokątów: "
                ))
        
        #wyglad jednej danej z listy prostokaty:
        #[przenikliwość,(x1,x2),(y1,y2)]
        
        #wprowadzamy przenikliwość, przedziały x i przedziały y
        for i in range(0,ile_prostokatow):
            przenikliwosc = input("Podaj przenikliwość prostokata nr."+str(i+1)+": ")
            #podzial stringa na dwie czesci przerobienie elementow tablicy na floata
            przedzial_x = tuple(map(
                float,
                input("Podaj przedział x - x1,x2, gdzie x1<x2: ").split(",")
            ))
            
            przedzial_y = tuple(map(
                float,
                input("Podaj przedział y - y1,y2, gdzie y1<y2: ").split(",")
            ))
            
            self.prostokaty.append(
                (przenikliwosc,
                 przedzial_x,
                 przedzial_y
                )
            )
            print(self.prostokaty)
        
        self.ilosc_zrodel = int(
            input("Podaj ilość źródeł: ")
        )
        
    def uruchom_pierwsza_czesc_zadania(self):
        #odległość między źródłami
        dystans_miedzy_puntkami = 2/(self.ilosc_zrodel-1)
        #wyznaczenie wszystkich źrodeł, zaczynając od -1 i dodawanie odległości
        zbior_x = [(-1)+x*dystans_miedzy_puntkami for x in range(0,self.ilosc_zrodel)]

        #warrtości dla poszczególnych "odbiornikow i sond"
        y1 = -1
        y2 = 1
        tablica_na_wyniki = []
        lista_na_wszystkie_przeciecia_pixeli = []
        
        #petla zaczynająca od lewego dolnego rogu i mierzaca sygnał do każdego odbiornika u góry
        s = time()
        for wspolrzedna_x1 in zbior_x:  
            for wspolrzedna_x2 in zbior_x:
                suma = 0
                #print(wspolrzedna_x1,wspolrzedna_x2)
                #wyliczamy prostą biegnącą od punktu(x1,y1) do (x2,y2)
                wynik_obliczanej_prostej = self.wylicz_prosta(
                    wspolrzedna_x1, y1,
                    wspolrzedna_x2, y2
                )
                
                odleglosci_miedzy_przecieciami = []
                
                ##CZESC 2
                
                #wyliczamy kolejne odleglosci przeciecia się prostej z pixelami
                i=0
                for kwadrat_pixel in self.wygenerowane_pixele_kwadraty:
                    odleglosc_miedzy_punktami = 0
                    #jeżeli wynik zawiera dwa elementy tablicy to obliczono a i b 
                    if len(wynik_obliczanej_prostej) == 2:
                        #liczymy miejsca przecięcia
                        miejsca_przeciecia = self.wylicz_punkty_przeciecia(
                            kwadrat_pixel,
                            wynik_obliczanej_prostej
                        )
                        odleglosc_miedzy_punktami = self.wylicz_odleglosc_miedzy_punktami(
                            miejsca_przeciecia
                        )
                        
                    else:
                        #sprawdzamy czy pionowa linia sie przetnie z jakimś prostokątem
                        odleglosc_miedzy_punktami = self.wylicz_odleglosc_pion(
                            kwadrat_pixel,
                            wynik_obliczanej_prostej
                        )
                    
                    if odleglosc_miedzy_punktami!=0:
                        odleglosci_miedzy_przecieciami.append((i,odleglosc_miedzy_punktami))
                    i = i + 1   
                lista_na_wszystkie_przeciecia_pixeli.append(odleglosci_miedzy_przecieciami)
                
                #dobieramy jeden prostokat i sprawdzamy czy sie przetnie
                for prostokat in self.prostokaty:
                    
                    odleglosc_miedzy_punktami = 0
                    #jeżeli wynik zawiera dwa elementy tablicy to obliczono a i b 
                    if len(wynik_obliczanej_prostej) == 2:
                        #liczymy miejsca przecięcia
                        miejsca_przeciecia = self.wylicz_punkty_przeciecia(
                            prostokat[1:],
                            wynik_obliczanej_prostej
                        )
                        odleglosc_miedzy_punktami = self.wylicz_odleglosc_miedzy_punktami(
                            miejsca_przeciecia
                        )
                        #print("Prosta:",odleglosc_miedzy_punktami)
                        
                    #w przeciwnym razie jezeli prosta nie została policzona to mamy
                    #pionowa linie
                    else:
                        #sprawdzamy czy pionowa linia sie przetnie z jakimś prostokątem
                        odleglosc_miedzy_punktami = self.wylicz_odleglosc_pion(
                            prostokat[1:],
                            wynik_obliczanej_prostej
                        )
                        #print("Pion:",odleglosc_miedzy_punktami)
                    suma = suma + prostokat[0]*odleglosc_miedzy_punktami
                    
                
                tablica_na_wyniki.append(round(suma,10))
            
            
            #tablica_na_wyniki.append(lista_sum)
          
        # t = 1
        # for i in range(0,len(lista_na_wszystkie_przeciecia_pixeli)):
        #     print(f"{t} - {tablica_na_wyniki[i]} i {lista_na_wszystkie_przeciecia_pixeli[i]}")
        #     t=t+1
        # print("------------------------")
        # for wynik in tablica_na_wyniki:
        #     print(tablica_na_wyniki)
        
        print(
            "Czas na prosta i pixele: ",time()-s
        )
        
        
        ##################################
        #CZESC III
        #Obliczenie równania
        #def tablica na rozwiazania
        #uzupelnienie pierwszego zerowego rekordu
        s = time()
        x = np.zeros(self.ilosc_pixeli_na_bok**2,dtype=float)

        for j in range(0,200):
            for i in range(0,self.ilosc_zrodel**2):
                
                    bi = tablica_na_wyniki[i]
                   
                    ai = lista_na_wszystkie_przeciecia_pixeli[i]
                    
                    ai_xk =  self.iloczyn_skalarny(ai,x)
                    ai_dl_without_sqrt = self.dlugosc_wektora_czesciowego(lista_na_wszystkie_przeciecia_pixeli[i])
                    lewa_strona = (bi-ai_xk)/(ai_dl_without_sqrt)

                    x = x + self.mnozenie_wartosci_wektor_czesciowy(ai,lewa_strona)
   
        print("Czas na policzenia kaczmarza",time()-s)
        
        fig = plt.figure(figsize=(8,6))
        plt.imshow(
            np.rot90(
                np.reshape(np.ndarray.round(x,decimals=5),(self.ilosc_pixeli_na_bok,self.ilosc_pixeli_na_bok))
            )
            )
        #plt.show()
            
            
        #print(np.ndarray.round(x,decimals=3))
           


    def iloczyn_skalarny(self,wektor_czesciowy,wektor_pelny):
        suma = 0
        for element_i_v in wektor_czesciowy:
            #suma to wartość z wektora czesciowego razy wartosc z wektora pełnego z odpowiadajacym mu indexem
            suma = suma + (element_i_v[1] * wektor_pelny[element_i_v[0]] )
        return suma
        
    def dlugosc_wektora_czesciowego(self,wektor_czesciowy):
        dlugosc = 0
        for element_i_v in wektor_czesciowy:
            #dlugosc to suma kwadratów wszystkich element i to pod pierwiastkiem
            dlugosc = dlugosc + element_i_v[1]**2    

        #zwroc spierwiastkowana sume
        return dlugosc
    
    def mnozenie_wartosci_wektor_czesciowy(self,wektor_czesciowy,wartosc):
        #funkcja zwraca wektor całościowy
        tablica = np.zeros(self.ilosc_pixeli_na_bok**2,dtype=float)
        for element in wektor_czesciowy:
            tablica[
                element[0]
            ] = element[1] * wartosc
            
        return tablica
            
        
    
    
    
    def wylicz_prosta(self,x1,y1,x2,y2):
        #y = ax + b
        #a = (yb-ya)/(xb-xa)
        if (y2-y1)!=0 and (x2-x1)!=0:
            a = (y1-y2)/(x1-x2)
        else:
            return [x2]
        # b = y-ax
        b = y2-a*(x2)
        return [a,b]

    def wylicz_odleglosc_pion(self, przedzialy, miejsce_na_xx):
        
        miejsce_na_x = miejsce_na_xx[0]
        #bierzemy (x1,x2)
        przedzial_x = przedzialy[0]
        #bierzemy (y1,y2)
        przedzial_y = przedzialy[1]
        dlugosc = 0
        
        #sprawdzamy czy pionowa linia miesci sie miedzy bokami prostokata
        #jezeli tak to liczymy jaka jest odleglosc pomiedzy dolnym a gornym bokiem
        if przedzial_x[1]>=miejsce_na_x and przedzial_x[0]<=miejsce_na_x:
            dlugosc = abs(przedzial_y[0]-przedzial_y[1])
        return dlugosc

    
    def wylicz_punkty_przeciecia(self, przedzialy,a_b_funkcjiliniowej):
        "Podajemy przedzialy prostokata [(x1,x2),(y1,y2)] i [a,b] prostej"
        
        a = a_b_funkcjiliniowej[0]
        b = a_b_funkcjiliniowej[1]
        
        #bierzemy (x1,x2)
        przedzial_x = przedzialy[0]
        #bierzemy (y1,y2)
        przedzial_y = przedzialy[1]
        

        przeciecia = []
        #przeciecia_z_y = []
        #podstawiajac za x z przedzialu prostokata sprawdzamy 
        #jaka wartosc y przeniesie wowczas funkcja liniowa
        #jezeli jest ona z przedzialu y prostokata to wowczas wiemy,
        #że funkcja przebiła się 
        for x in przedzial_x:
            y = a*x+b
            y = round(y,10)
            #print(y)
            if round(przedzial_y[0],10)<=y and y<=round(przedzial_y[1],10):
                #print("Przecięcie!",x,y)
                przeciecia.append((round(x,10),y))
                
        for y in przedzial_y:
            x = (y-b)/a
            x = round(x,10)
            #print(x)
            if round(przedzial_x[0],10)<=x and x<=round(przedzial_x[1],10):
                #print("Przecięcie!",x,y)
                przeciecia.append((x,round(y,10)))
        
        
        #wyrzucamy z listy duplikaty za pomocą seta(set działa tylko na tuple!!!)
        przeciecia = list(
            set(przeciecia)
        )
         
        # print(
        #     "Przeciecia z x: ",przeciecia_z_x,
        #     "Przeciecia z y: ",przeciecia_z_y,
        #     "Wspolczynniki a i b: ",a_b_funkcjiliniowej,
        #     "Przedzial x prostokata ",przedzial_x,
        #     "Przedzial y prostokata ",przedzial_y
        # )
        
        return przeciecia
    
    def wylicz_odleglosc_miedzy_punktami(self, punkty):
        
        #jezeli nie ma punktow to nie ma odleglosci
        if len(punkty)==0:
            return 0
        
        #punkt przeszedl przez róg prostokąta
        elif len(punkty)==1:
            return 0 
        elif len(punkty)!=2:
            
            print("error ",punkty)
            return 0
        #rozdzielenie na x i na y
        zbior_x = [x[0] for x in punkty]
        zbior_y = [y[1] for y in punkty]
        
        #zwykly wzor na odleglosc
        odleglosc = (
            (zbior_x[0]-zbior_x[1])**2 + (zbior_y[0]-zbior_y[1])**2
                     )**(1/2)
        
        return odleglosc
        
    



program_tomografia = Tomografia()
program_tomografia.wygeneruj_pixele_kwadraty()
start = time()
program_tomografia.uruchom_pierwsza_czesc_zadania()
koniec = time()
print("Liczba upłyniętych sekund: ",koniec-start)
#aktualny czas to 0.005s        
    
  


        
        
        
        
