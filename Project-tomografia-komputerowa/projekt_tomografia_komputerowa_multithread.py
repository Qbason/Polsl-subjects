from threading import Thread
import numpy as np
import matplotlib.pyplot as plt
from time import time
from concurrent.futures import ProcessPoolExecutor, ThreadPoolExecutor,wait
from math import sqrt

class Tomografia:
    
    def __init__(self,ilosc_zrodel,ilosc_pixeli_na_bok):
        self.prostokaty = ([
            [1, [-0.4,-0.2],[-0.5,0.5]],
            [2, [-0.2,0.2],[0.3,0.5]],
            [3, [-0.2,0.2],[-0.1,0.1]],
            [4, [0,0.2],[0.1,0.3]],
            [5, [0.6,0.8],[-0.8,-0.6]]
        ])
        
        
        self.ilosc_zrodel = ilosc_zrodel
        self.ilosc_pixeli_na_bok = ilosc_pixeli_na_bok
        #self.wygenerowane_pixele_kwadraty = []
        self.x = np.zeros(self.ilosc_pixeli_na_bok**2,dtype=float)
        #lista przygotowana na sume dlugosci wraz
        self.lista_na_sume_odleglosci_z_wagami = []
        self.lista_na_wszystkie_przeciecia_pixeli = []
        self.all_dl_without_sqrt_gen = []
        #self.odczytaj_dane()
        
    

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
    
    def oblicz_odlegosci_przeciecia_prostej_z_pixelami(self):
        #odległość między źródłami
        dystans_miedzy_puntkami = 2/(self.ilosc_zrodel-1)
        #wyznaczenie wszystkich źrodeł, zaczynając od -1 i dodawanie odległości
        zbior_x = [(-1)+x*dystans_miedzy_puntkami for x in range(0,self.ilosc_zrodel)]

        lista_na_wszystkie_przeciecia_pixeli = []

        #warrtości dla poszczególnych "odbiornikow i sond"
        y1 = -1
        y2 = 1
        for wspolrzedna_x1 in zbior_x:  
            for wspolrzedna_x2 in zbior_x:
                
                #wyliczamy prostą biegnącą od punktu(x1,y1) do (x2,y2)
                wynik_obliczanej_prostej = self.wylicz_prosta(
                    wspolrzedna_x1, y1,
                    wspolrzedna_x2, y2
                )
                
                wynik = self.oblicz_odleglosc_miedzy_przecieciami(wynik_obliczanej_prostej)
                lista_na_wszystkie_przeciecia_pixeli.append(wynik)
                
        return lista_na_wszystkie_przeciecia_pixeli
        
    def oblicz_odleglosc_miedzy_przecieciami_pixeli(self,wynik_obliczanej_prostej):
        odleglosci_miedzy_przecieciami = []
        i=0
        #wyliczamy kolejne odleglosci przeciecia się prostej z pixelami
        #for kwadrat_pixel in self.wygenerowane_pixele_kwadraty:
        dystans_miedzy_pixelami = 2/self.ilosc_pixeli_na_bok
        zbior_x = [(-1)+x*dystans_miedzy_pixelami for x in range(0,self.ilosc_pixeli_na_bok+1)]
        #zbior_y = [(1)-x*dystans_miedzy_pixelami for x in range(0,self.ilosc_pixeli_na_bok+1)]
        
        for i_x in range(0,len(zbior_x)-1):
            for i_y in range(0,len(zbior_x)-1):
                kwadrat_pixel= [(zbior_x[i_x],zbior_x[i_x+1]),(zbior_x[i_y],zbior_x[i_y+1])]
        
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
                #jeżeli odległość wynosi zero to ignoruj
                if odleglosc_miedzy_punktami!=0:
                    odleglosci_miedzy_przecieciami.append((i,odleglosc_miedzy_punktami))
                i = i + 1
                #print(odleglosci_miedzy_przecieciami)
        return odleglosci_miedzy_przecieciami   
        

    
    def oblicz_przeciecia_prostej_z_prostokatami_i_pixelami_oraz_kaczmarza(self):
        
        #odległość między źródłami
        dystans_miedzy_puntkami = 2/(self.ilosc_zrodel-1)
        #wyznaczenie wszystkich źrodeł, zaczynając od -1 i dodawanie odległości
        zbior_x = [(-1)+x*dystans_miedzy_puntkami for x in range(0,self.ilosc_zrodel)]

        #wartości dla poszczególnych "odbiornikow i sond"
        y1 = -1
        y2 = 1
        wyniki_obliczanej_prostej = []
        
        #wyliczenie wspolczynikow a i b wszystkich prostych
        s = time()
        for wspolrzedna_x1 in zbior_x:  
            for wspolrzedna_x2 in zbior_x:
                    wyniki_obliczanej_prostej.append(self.wylicz_prosta(wspolrzedna_x1,y1,wspolrzedna_x2,y2))

            
        print(
            "Czas na wyliczenie prostych: ",time()-s
        )
        
        #for wynik_obliczanej_prostej in wyniki_obliczanej_prostej:  
        s = time()
        with ProcessPoolExecutor() as executor:
                 
            ##CZESC 1 - obliczanie odleglosci * waga dla danego prostokata
            f1 = executor.map(self.oblicz_pojedyncza_odleglosc_przeciecia_prostej_z_prostokatem,wyniki_obliczanej_prostej)
            ##CZESC 2 - obliczanie odleglosci przeciecia się prostej z pixelami
            f2 = executor.map(self.oblicz_odleglosc_miedzy_przecieciami_pixeli,wyniki_obliczanej_prostej)

        print("Czas na wyliczenie odleglosci pixeli itd. ",time()-s)  
          
        
          
          
          
        #CZESC 3 - obliczanie na macierzy dzieki metodzie Kaczmarza
        s = time()
        #self.obliczanie_macierzy_Kaczmarz_wer_gen(f2,f1)
        self.lista_na_sume_odleglosci_z_wagami = tuple(f1)
        self.lista_na_wszystkie_przeciecia_pixeli = tuple(f2)
        
        
        print(
            "Czas na przeksztalcenie do krotki: ",time()-s
        )
        
        s = time()
        self.all_dl_without_sqrt_gen = tuple(map(self.dlugosc_wektora_czesciowego,self.lista_na_wszystkie_przeciecia_pixeli))
        print(
            "Czas na policzenie dlugosci wektorow: ",time()-s
        )
        
        s = time()
        self.obliczanie_macierzy_Kaczmarz(self.lista_na_wszystkie_przeciecia_pixeli,self.lista_na_sume_odleglosci_z_wagami)
        print(
            "Czas na policzenie Kaczmarzy: ",time()-s
        )
    
    
    def oblicz_pojedyncza_odleglosc_przeciecia_prostej_z_prostokatem(self,wynik_obliczanej_prostej):
        suma = 0
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
        return round(suma,10)
    
    # def obliczanie_macierzy_Kaczmarz_wer_pojedyncza(self,lista_na_przeciecia_pixeli,suma_odleglosci_z_waga):
        
    #     #dlugosc_petli = self.ilosc_zrodel**2
        
    #     bi = suma_odleglosci_z_waga
    #     #print(bi)
    #     ai = lista_na_przeciecia_pixeli
    #     ai_xk =  self.iloczyn_skalarny(ai,self.x)
    #     ai_dl_without_sqrt = self.dlugosc_wektora_czesciowego(lista_na_przeciecia_pixeli)
    #     lewa_strona = (bi-ai_xk)/(ai_dl_without_sqrt)
    #     self.x = self.x + self.mnozenie_wartosci_wektor_czesciowy(ai,lewa_strona)
        
                        
    
    def obliczanie_macierzy_Kaczmarz(self,lista_na_wszystkie_przeciecia_pixeli,lista_na_sume_odleglosci_z_wagami):
        
        for j in range(0,200):
            for wagi,przeciecia,ai_dl_without_sqrt in zip(lista_na_sume_odleglosci_z_wagami,lista_na_wszystkie_przeciecia_pixeli,self.all_dl_without_sqrt_gen):
                bi = wagi
                ai = przeciecia
                #ai_xk = executor.submit(self.iloczyn_skalarny,ai,self.x)
                #ai_dl_without_sqrt = executor.submit(self.dlugosc_wektora_czesciowego,lista_na_wszystkie_przeciecia_pixeli[i])
                # wait([ai_xk,ai_dl_without_sqrt])
                ai_xk =  self.iloczyn_skalarny(ai,self.x)
                #ai_dl_without_sqrt = self.dlugosc_wektora_czesciowego(przeciecia)
                lewa_strona = (bi-ai_xk)/(ai_dl_without_sqrt)
                self.x = self.x + self.mnozenie_wartosci_wektor_czesciowy(ai,lewa_strona)
                

    # def obliczanie_macierzy_Kaczmarz_wer_gen(self,lista_na_wszystkie_przeciecia_pixeli,lista_na_sume_odleglosci_z_wagami):
            
    #     dlugosc_petli = self.ilosc_zrodel**2
        
    #     for i in range(0,dlugosc_petli):
    #         bi = next(lista_na_sume_odleglosci_z_wagami)
    #         self.lista_na_sume_odleglosci_z_wagami.append(bi)
    #         #print(bi)
    #         ai = next(lista_na_wszystkie_przeciecia_pixeli)
    #         self.lista_na_wszystkie_przeciecia_pixeli.append(ai)
            
    #         ai_xk =  self.iloczyn_skalarny(ai,self.x)
    #         ai_dl_without_sqrt = self.dlugosc_wektora_czesciowego(ai)
    #         lewa_strona = (bi-ai_xk)/(ai_dl_without_sqrt)
    #         self.x = self.x + self.mnozenie_wartosci_wektor_czesciowy(ai,lewa_strona)  
            

    def rysuj(self):
        #przedstawienie rysunku obliczonych danych
        fig = plt.figure(figsize=(8,6))
        plt.imshow(
            np.rot90(
                np.reshape(np.ndarray.round(self.x,decimals=6),(self.ilosc_pixeli_na_bok,self.ilosc_pixeli_na_bok))
            )
            )
        plt.savefig(f'{self.ilosc_pixeli_na_bok}x{self.ilosc_zrodel}-tomografia.png')
        #plt.show()
    

    def iloczyn_skalarny(self,wektor_czesciowy,wektor_pelny):

        # for element_i_v in wektor_czesciowy:
        #     #suma to wartość z wektora czesciowego razy wartosc z wektora pełnego z odpowiadajacym mu indexem
        #     suma = suma + (element_i_v[1] * wektor_pelny[element_i_v[0]] )
        
        return sum(element_i_v[1]*wektor_pelny[element_i_v[0]] for element_i_v in wektor_czesciowy)
        
    def dlugosc_wektora_czesciowego(self,wektor_czesciowy):
        # dlugosc = 0
        # for element_i_v in wektor_czesciowy:
        #     #dlugosc to suma kwadratów wszystkich element i to pod pierwiastkiem
        #     dlugosc = dlugosc + element_i_v[1]**2    

        #zwroc spierwiastkowana sume
        return sum(element_i_v[1]*element_i_v[1] for element_i_v in wektor_czesciowy)
    
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
            return[x2]
        # b = y-ax
        b = y2-a*(x2)
        return (a,b)

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
            if round(przedzial_y[0],10)<=y and y<=round(przedzial_y[1],10):
                przeciecia.append((round(x,10),y))
                
        for y in przedzial_y:
            x = (y-b)/a
            x = round(x,10)
            if round(przedzial_x[0],10)<=x and x<=round(przedzial_x[1],10):
                przeciecia.append((x,round(y,10)))
        
        
        #wyrzucamy z listy duplikaty za pomocą seta(set działa tylko na tuple!!!)
        przeciecia = list(
            set(przeciecia)
        )
         
        
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
        # zbior_x = [x[0] for x in punkty]
        # zbior_y = [y[1] for y in punkty]
        
        #zwykly wzor na odleglosc
        odleglosc = sqrt(
            (punkty[0][0]-punkty[1][0])**2 + (punkty[0][1]-punkty[1][1])**2
                     )
        
        return odleglosc
        
    


if __name__ == '__main__':
    #program_tomografia.wygeneruj_pixele_kwadraty()
    start = time()

    program_tomografia = Tomografia(40,40)
    program_tomografia.oblicz_przeciecia_prostej_z_prostokatami_i_pixelami_oraz_kaczmarza()
    program_tomografia.rysuj()
    #del(program_tomografia)
    koniec = time()
    print("Liczba upłyniętych sekund: ",koniec-start)
    

#aktualny czas to 0.005s        
    
  


        
        
        
        
