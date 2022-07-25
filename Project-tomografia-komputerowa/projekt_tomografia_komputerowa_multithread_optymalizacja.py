import numpy as np
import matplotlib.pyplot as plt
from time import time
from concurrent.futures import ProcessPoolExecutor, wait
from math import sqrt



class Tomografia:
    
    def __init__(self,ilosc_zrodel,ilosc_pixeli_na_bok,index):
        self.prostokaty = ([
            [1, [-0.4,-0.2],[-0.5,0.5]],
            [2, [-0.2,0.2],[0.3,0.5]],
            [3, [-0.2,0.2],[-0.1,0.1]],
            [4, [0,0.2],[0.1,0.3]],
            [5, [0.6,0.8],[-0.8,-0.6]]
        ])
        
        self.special_number_index = index
        self.ilosc_zrodel = ilosc_zrodel
        self.ilosc_pixeli_na_bok = ilosc_pixeli_na_bok


            
    def oblicz_odleglosc_miedzy_przecieciami_pixeli(self,wynik_obliczanej_prostej):
        numpy_array_przeciecia = np.zeros(
            self.ilosc_pixeli_na_bok**2,
            dtype=np.float32)
        
        i=0
        #wyliczamy kolejne odleglosci przeciecia się prostej z pixelami
        dystans_miedzy_pixelami = 2/self.ilosc_pixeli_na_bok
        zbior_x = [(-1)+x*dystans_miedzy_pixelami for x in range(0,self.ilosc_pixeli_na_bok+1)]
        
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

                    numpy_array_przeciecia[i] = odleglosc_miedzy_punktami

                i = i + 1
        return numpy_array_przeciecia
        

    
    def oblicz_przeciecia_prostej_z_prostokatami_i_pixelami_oraz_kaczmarza(self):
        lista_na_wszystkie_przeciecia_pixeli = np.zeros(
            (self.ilosc_zrodel**2,self.ilosc_pixeli_na_bok*self.ilosc_pixeli_na_bok),
            dtype=np.float32
        )
        lista_na_sume_odleglosci_z_wagami = np.zeros(
            (self.ilosc_zrodel**2),
            dtype=np.float32
        )
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
        
        s = time()
        with ProcessPoolExecutor() as executor:
                 
            #CZESC 1 - obliczanie odleglosci * waga dla danego prostokata
            f1 = executor.map(self.oblicz_pojedyncza_odleglosc_przeciecia_prostej_z_prostokatem,wyniki_obliczanej_prostej)
            #CZESC 2 - obliczanie odleglosci przeciecia się prostej z pixelami
            f2 = executor.map(self.oblicz_odleglosc_miedzy_przecieciami_pixeli,wyniki_obliczanej_prostej)

        print("Czas na wyliczenie odleglosci pixeli itd. ",time()-s) 
         
        s = time()
        
        for i,(dlugosc,dlugosc_tab) in enumerate(zip(f1,f2)):
            lista_na_sume_odleglosci_z_wagami[i] = dlugosc
            lista_na_wszystkie_przeciecia_pixeli[i] = dlugosc_tab
     
        print("Czas przerobienie generatora na array ",time()-s)  
        
          
        #CZESC 3 - obliczanie na macierzy dzieki metodzie Kaczmarza
  
        s = time()
        x = self.obliczanie_macierzy_Kaczmarz(lista_na_wszystkie_przeciecia_pixeli,lista_na_sume_odleglosci_z_wagami)
        print(
            "Czas na policzenie Kaczmarzy: ",time()-s
        )
        self.rysuj(x)
    
    
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
            suma = suma + prostokat[0]*odleglosc_miedzy_punktami   
        return round(suma,8)
                    
    
    def obliczanie_macierzy_Kaczmarz(self,lista_na_wszystkie_przeciecia_pixeli,lista_na_sume_odleglosci_z_wagami):
        x = np.zeros(self.ilosc_pixeli_na_bok**2,dtype=np.float32)
                
        for j in range(0,200):
            for wagi,przeciecia in zip(lista_na_sume_odleglosci_z_wagami,lista_na_wszystkie_przeciecia_pixeli):
                    bi = wagi
                    ai = przeciecia
                    ai_xk = ai.dot(x)
                    #s = time()
                    #ai_xk = ai.multiply(x).sum()
                    #print("Tyle czasu trzeba na ai_xk",ai_xk)
                    ai_dl_without_sqrt = ai.dot(ai)
                    #s = time()
                    #ai_dl_without_sqrt = ai.multiply(ai).sum()
                    #print("Tyle czasu trzeba na ai_dl_without_sqrt",time()-s)
                    lewa_strona = (bi-ai_xk)/(ai_dl_without_sqrt)
                    x = x + ai*lewa_strona
        return x
            



    def rysuj(self,x):
        #przedstawienie rysunku obliczonych danych
        plt.title(f"{self.ilosc_pixeli_na_bok}x{self.ilosc_zrodel}")
        plt.imshow(
            np.rot90(
                np.reshape(
                    np.ndarray.round(x,decimals=8),(self.ilosc_pixeli_na_bok,self.ilosc_pixeli_na_bok)
                    )
            )
            )
        plt.savefig(f'{self.special_number_index}_{self.ilosc_pixeli_na_bok}x{self.ilosc_zrodel}-tomografia.png')
        plt.clf()
        #plt.show()
    

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
            y = round(y,8)
            if round(przedzial_y[0],8)<=y and y<=round(przedzial_y[1],8):
                przeciecia.append((round(x,8),y))
                
        for y in przedzial_y:
            x = (y-b)/a
            x = round(x,8)
            if round(przedzial_x[0],8)<=x and x<=round(przedzial_x[1],8):
                przeciecia.append((x,round(y,8)))
        
        
        #wyrzucamy z listy duplikaty za pomocą seta(set działa tylko na tuple!!!)
        przeciecia = tuple(
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
        
        return round(odleglosc,8)
        
    
if __name__ == '__main__':
    fig = plt.figure(figsize=(8,6))
    start = time()
    program_tomografia = Tomografia(40,40,1)
    program_tomografia.oblicz_przeciecia_prostej_z_prostokatami_i_pixelami_oraz_kaczmarza()
    koniec = time()
    
    print("Liczba upłyniętych sekund: ",koniec-start)
    

    
  


        
        
        
        
