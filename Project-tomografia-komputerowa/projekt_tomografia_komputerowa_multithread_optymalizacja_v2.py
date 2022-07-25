import numpy as np
import matplotlib.pyplot as plt
from time import time
from concurrent.futures import ProcessPoolExecutor
from math import sqrt
from scipy.sparse import csr_array

special_number_index = 2137
ilosc_zrodel = 20
ilosc_pixeli_na_bok = 20

prostokaty = (
        (1, -0.4,-0.2,-0.5,0.5),
        (2, -0.2,0.2,0.3,0.5),
        (3, -0.2,0.2,-0.1,0.1),
        (4, 0,0.2,0.1,0.3),
        (5, 0.6,0.8,-0.8,-0.6)
)
    
dystans_miedzy_pixelami = 2/ilosc_pixeli_na_bok
wspolrzedne_x_pixeli = [round((-1)+x*dystans_miedzy_pixelami,6) for x in range(0,ilosc_pixeli_na_bok+1)] 
wspolrzedne_pixeli = [
    
    (wspolrzedne_x_pixeli[i_x],wspolrzedne_x_pixeli[i_x+1],wspolrzedne_x_pixeli[i_y],wspolrzedne_x_pixeli[i_y+1]) 
    for i_x in range(len(wspolrzedne_x_pixeli)-1) for i_y in range(len(wspolrzedne_x_pixeli)-1)
    
]


def oblicz_odleglosc_miedzy_przecieciami_pixeli(wynik_obliczanej_prostej):
    numpy_array_przeciecia = np.zeros(
        (ilosc_pixeli_na_bok**2),
        dtype=np.float32)

    xx = wynik_obliczanej_prostej[2:] if len(wynik_obliczanej_prostej) == 4 else wynik_obliczanej_prostej[0:]
    x_koncowy = max(xx)
    x_poczatkowy = min(xx)
    index_zakonczenia = len(wspolrzedne_x_pixeli)-1
    index_rozpoczecia = index_zakonczenia - 1
    koniec_zakonczenia = False
    koniec_rozpoczecia = False
    for i,x in enumerate(wspolrzedne_x_pixeli):
        if x_koncowy<x and not koniec_zakonczenia:
            index_zakonczenia  = i
            koniec_zakonczenia = True
        if x_poczatkowy<x and not koniec_rozpoczecia:
            index_rozpoczecia = i-1
            koniec_rozpoczecia = True    
        if koniec_rozpoczecia and koniec_zakonczenia:
           break

    #print(f"Index rozpoczecia sie prostej:{index_rozpoczecia} Index konczenia sie prostej:{index_zakonczenia} X_koncowe_prostej = {x_koncowy} xx: {xx} dl_tablicy={len(wspolrzedne_pixeli[(index_rozpoczecia*ilosc_pixeli_na_bok):(index_zakonczenia*ilosc_pixeli_na_bok)])}")
    
    i=index_rozpoczecia*ilosc_pixeli_na_bok
    #wyliczamy kolejne odleglosci przeciecia się prostej z pixelami
    for kwadrat_pixel in wspolrzedne_pixeli[(index_rozpoczecia*ilosc_pixeli_na_bok):(index_zakonczenia*ilosc_pixeli_na_bok)]:
    
            odleglosc_miedzy_punktami = 0
            #jeżeli wynik zawiera dwa elementy tablicy to obliczono a i b 
            if len(wynik_obliczanej_prostej) == 4:
                #liczymy miejsca przecięcia
                miejsca_przeciecia = wylicz_punkty_przeciecia(
                    kwadrat_pixel,
                    wynik_obliczanej_prostej
                )
                odleglosc_miedzy_punktami = wylicz_odleglosc_miedzy_punktami(
                    miejsca_przeciecia
                )
            else:
                #sprawdzamy czy pionowa linia sie przetnie z jakimś prostokątem
                odleglosc_miedzy_punktami = wylicz_odleglosc_pion(
                    kwadrat_pixel,
                    wynik_obliczanej_prostej
                )
            #jeżeli odległość wynosi zero to ignoruj
            if odleglosc_miedzy_punktami!=0:
                numpy_array_przeciecia[i] = odleglosc_miedzy_punktami

            i = i + 1

    return csr_array(numpy_array_przeciecia)
    #return np.asarray(przeciecia)
    #return przeciecia

    
def oblicz_pojedyncza_odleglosc_przeciecia_prostej_z_prostokatem(wynik_obliczanej_prostej):
    suma = 0
    #dobieramy jeden prostokat i sprawdzamy czy sie przetnie
    
    for prostokat in prostokaty:
        odleglosc_miedzy_punktami = 0
        #jeżeli wynik zawiera dwa elementy tablicy to obliczono a i b 
        if len(wynik_obliczanej_prostej) == 4:
            #liczymy miejsca przecięcia
            miejsca_przeciecia = wylicz_punkty_przeciecia(
                prostokat[1:],
                wynik_obliczanej_prostej
            )
            odleglosc_miedzy_punktami = wylicz_odleglosc_miedzy_punktami(
                miejsca_przeciecia
            )
            
        #w przeciwnym razie jezeli prosta nie została policzona to mamy
        #pionowa linie
        else:
            #sprawdzamy czy pionowa linia sie przetnie z jakimś prostokątem
            odleglosc_miedzy_punktami = wylicz_odleglosc_pion(
                prostokat[1:],
                wynik_obliczanej_prostej
            )
        suma = suma + prostokat[0]*odleglosc_miedzy_punktami   
    return round(suma,7)


def obliczanie_macierzy_Kaczmarz(lista_na_wszystkie_przeciecia_pixeli,lista_na_sume_odleglosci_z_wagami):
    x = np.zeros(ilosc_pixeli_na_bok**2,dtype=np.float32)
     
    for j in range(0,200):
        for bi,ai_csr in zip(lista_na_sume_odleglosci_z_wagami,lista_na_wszystkie_przeciecia_pixeli):
            
            ai = np.squeeze(ai_csr.toarray())
            ai_xk = ai.dot(x)
            ai_dl_without_sqrt = ai.dot(ai)
            lewa_strona = (bi-ai_xk)/(ai_dl_without_sqrt)
            x = (x + lewa_strona*ai)
        
        for i,v in enumerate(x):
            if v<0:
                x[i]=0
                
    return x
    
def dlugosc_wektora_czesciowego(wektor_czesciowy):
    dlugosc = 0
    for element_i_v in wektor_czesciowy:
        #dlugosc to suma kwadratów wszystkich element i to pod pierwiastkiem
        dlugosc = dlugosc + element_i_v[1]**2    

    #zwroc spierwiastkowana sume
    return dlugosc
    
        
def iloczyn_skalarny(wektor_czesciowy,wektor_pelny):
        suma = 0
        for element_i_v in wektor_czesciowy:
            #suma to wartość z wektora czesciowego razy wartosc z wektora pełnego z odpowiadajacym mu indexem
            suma = suma + (element_i_v[1] * wektor_pelny[element_i_v[0]] )
        return suma
    
def mnozenie_wartosci_wektor_czesciowy(wektor_czesciowy,wartosc):
    #funkcja zwraca wektor całościowy
    tablica = np.zeros(ilosc_pixeli_na_bok**2,dtype=np.float32)
    for element in wektor_czesciowy:
        tablica[
            element[0]
        ] = element[1] * wartosc
        
    return tablica

def rysuj(x):
    #przedstawienie rysunku obliczonych danych
    plt.title(f"{ilosc_pixeli_na_bok}x{ilosc_zrodel}")
    plt.imshow(
        np.rot90(
            np.reshape(
                np.ndarray.round(x,decimals=7),(ilosc_pixeli_na_bok,ilosc_pixeli_na_bok)
                )
        )
        )
    #plt.savefig(f'{special_number_index}_{ilosc_pixeli_na_bok}x{ilosc_zrodel}-tomografia.png')
    plt.show()
    plt.clf()
    

def wylicz_prosta(x1,y1,x2,y2):
    #y = ax + b
    #a = (yb-ya)/(xb-xa)
    if (y2-y1)!=0 and (x2-x1)!=0:
        a = (y1-y2)/(x1-x2)

    else:
        return (x1,x2)
    # b = y-ax
    b = y2-a*(x2)
    return (a,b,x1,x2)

def wylicz_odleglosc_pion(przedzialy, miejsce_na_xx):

        
        miejsce_na_x = miejsce_na_xx[0]
        #bierzemy (x1,x2)
        przedzial_x = przedzialy[0:2]
        #bierzemy (y1,y2)
        przedzial_y = przedzialy[2:4]
        
        dlugosc = 0
        
        #sprawdzamy czy pionowa linia miesci sie miedzy bokami prostokata
        #jezeli tak to liczymy jaka jest odleglosc pomiedzy dolnym a gornym bokiem
        if przedzial_x[1]>=miejsce_na_x and przedzial_x[0]<=miejsce_na_x:
            dlugosc = abs(przedzial_y[0]-przedzial_y[1])
        return round(dlugosc,7)



def wylicz_punkty_przeciecia(przedzialy,a_b_funkcjiliniowej):
    "Podajemy przedzialy prostokata [(x1,x2),(y1,y2)] i [a,b] prostej"
    a = a_b_funkcjiliniowej[0]
    b = a_b_funkcjiliniowej[1]
    #bierzemy (x1,x2)
    przedzial_x = przedzialy[0:2]
    #bierzemy (y1,y2)
    przedzial_y = przedzialy[2:4]
    przeciecia = []
    #podstawiajac za x z przedzialu prostokata sprawdzamy 
    #jaka wartosc y przeniesie wowczas funkcja liniowa
    #jezeli jest ona z przedzialu y prostokata to wowczas wiemy,
    #że funkcja przebiła się 
    for x in przedzial_x:
        y = a*x+b
        y = round(y,9)
        if przedzial_y[0]<=y and y<=przedzial_y[1]:
            przeciecia.append((x,y))
            
    for y in przedzial_y:
        x = (y-b)/a
        x = round(x,9)
        if przedzial_x[0]<=x and x<=przedzial_x[1]:
            przeciecia.append((x,y))
    
    #wyrzucamy z listy duplikaty za pomocą seta
    przeciecia = tuple(
        set(przeciecia)
    )
    

    return przeciecia

def wylicz_odleglosc_miedzy_punktami(punkty):
    
    #jezeli nie ma punktow to nie ma odleglosci
    dl = len(punkty)
    
    if dl==0:
        return 0
    #punkt przeszedl przez róg prostokąta
    elif dl==1:
        return 0 
    elif dl!=2:
        print("error ",punkty)
        return 0

    #zwykly wzor na odleglosc
    odleglosc = sqrt(
        (punkty[0][0]-punkty[1][0])**2 + (punkty[0][1]-punkty[1][1])**2
                    )
    
    return round(odleglosc,7)



if __name__ == '__main__':
    
    
    start = time()
    s = time()
    
    fig = plt.figure(figsize=(8,6))

    #przygotowanie tablicy na wyniki
    # lista_na_wszystkie_przeciecia_pixeli = np.zeros(
    #     (ilosc_zrodel**2,ilosc_pixeli_na_bok*ilosc_pixeli_na_bok),
    #     dtype=float
    # )
    lista_na_wszystkie_przeciecia_pixeli = []
    
    lista_na_sume_odleglosci_z_wagami = np.empty(
        (ilosc_zrodel**2),
        dtype=np.float32
    )
    
    #odległość między źródłami
    dystans_miedzy_puntkami = 2/(ilosc_zrodel-1)
    #wyznaczenie wszystkich źrodeł, zaczynając od -1 i dodawanie odległości
    zbior_x = [(-1)+x*dystans_miedzy_puntkami for x in range(0,ilosc_zrodel)]

    #wyliczenie wspolczynikow a i b wszystkich prostych
    y1 = -1
    y2 = 1
    wyniki_obliczanej_prostej = [
        wylicz_prosta(wspolrzedna_x1,y1,wspolrzedna_x2,y2) for wspolrzedna_x1 in zbior_x for wspolrzedna_x2 in zbior_x
    ]

    print(
        "Czas na wyliczenie prostych: ",time()-s
    )
    s = time()
    
    with ProcessPoolExecutor() as executor:  
        #CZESC 1 - obliczanie odleglosci * waga dla danego prostokata
        f1 = executor.map(oblicz_pojedyncza_odleglosc_przeciecia_prostej_z_prostokatem,wyniki_obliczanej_prostej)
    #f1 = map(oblicz_pojedyncza_odleglosc_przeciecia_prostej_z_prostokatem,wyniki_obliczanej_prostej)
        #CZESC 2 - obliczanie odleglosci przeciecia się prostej z pixelami
        f2 = executor.map(oblicz_odleglosc_miedzy_przecieciami_pixeli,wyniki_obliczanej_prostej)
    #f2 = map(oblicz_odleglosc_miedzy_przecieciami_pixeli,wyniki_obliczanej_prostej)

    print("Czas na wyliczenie odleglosci pixeli itd. ",time()-s) 
        
    s = time()
    
    for i,(dlugosc,dlugosc_tab) in enumerate(zip(f1,f2)):
        lista_na_sume_odleglosci_z_wagami[i] = dlugosc#.result()
        lista_na_wszystkie_przeciecia_pixeli.append(dlugosc_tab)#.result()
    
    print("Czas przerobienie generatora na array ",time()-s)  
    
        
    #CZESC 3 - obliczanie na macierzy dzieki metodzie Kaczmarza

    s = time()
    x = obliczanie_macierzy_Kaczmarz(lista_na_wszystkie_przeciecia_pixeli,lista_na_sume_odleglosci_z_wagami)
    print(
        "Czas na policzenie Kaczmarzy: ",time()-s
    )
    rysuj(x)
    
    

    koniec = time()
    print("Liczba upłyniętych sekund: ",koniec-start)




            
    
        

    
    
    
    
    
                    
    
    
        
    

    
  


        
        
        
        
