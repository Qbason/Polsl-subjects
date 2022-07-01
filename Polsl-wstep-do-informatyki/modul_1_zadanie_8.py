import tkinter as tk
import random 
#import potrzebnych bibliotek

#utworzenie klasy aplikacja, ktora bedzie edytowala zawartosc naszego okienka
class Application(tk.Frame):
    def __init__(self, master=None):
        #odwołanie sie od clasy tk.Frame i wywolanie funkcji inicjującej z argumentem self i 'master'
        # => Zapis ten sam co Frame.__init__(self,master)
        super().__init__(master)
        #przypisanie do zmiennej wewnetrzenej clasy Aplkacja  zmiennej master
        self.master = master
        #spakowanie wszystkich składnikow
        self.pack()
        #wywolanie funkcji tworzacej 'guziki,pola do wprowadzania danych
        self.create_widgets()
        #wylosowanie liczby poczatkowej do  zgadniecia
        self.number_rand = random.randint(1, 100)

    def create_widgets(self):

        #ustalenie warunku poczatkowego, tzn. uzytkownik nie zgadł, jeśli jeszce nie zaczał zgadywać
        self.guessed = False

        #zmienna tekstowa wyswietlana na ekranie 
        self.info_box_text = tk.StringVar(self)
        self.info_box_text.set( "Witaj w naszej grze,\n wylosowałem liczbę z przedziału od 1 do 100")
        
        #etykieta umozliwajaca wyswietlenie zmiennej teksowej self.info_box_text
        self.info_box = tk.Label(self)
        self.info_box["textvariable"] = self.info_box_text #przypisanie do etykiety
        #ustalenie miejsca w którym znajduje się nasza 'etykieta'
        self.info_box.pack(side="top")

        #Utworzenie pola do wpisywania danych liczbowych
        self.input_box = tk.Entry(self)
        #self.input_box["text"]="" Niepotrzebne input nie wystepuje z textem startowym :) 
        self.input_box.pack(side="top")

        #Utworzenie buttona ktory obsluguje funkcje guess(zgadnij)
        self.hi_there = tk.Button(self)
        self.hi_there["text"] = "Zgadnij" #nadanie nazywa naszemu przyciskowi
        self.hi_there["command"] = self.guess
        self.hi_there.pack(side="top")

        #Utworzenie buttona, ktory losuje liczbe
        self.draw_los = tk.Button(self)
        self.draw_los["text"] = "Losuj nowa liczbe"
        self.draw_los["command"] = self.draw_los_num
        self.draw_los.pack(side="top")

        #Utworzenie buttona, ktory zamyka aplikacje
        self.quit = tk.Button(self, text="QUIT", fg="red", command=self.master.destroy)
        self.quit.pack(side="bottom")

    #funkcja ktora sprawdza czy uzytkownik zgadnal liczbe jesli tak to wylosuje nowa, 
    # w przeciwym razie wyswietli informacje na ekranie
    def draw_los_num(self):
        if self.guessed == True:
            self.number_rand = random.randint(1, 100)
            self.guessed = False
            self.info_box_text.set("Wylosowałem strzelaj!")
        else:
            self.info_box_text.set("Najpierw wygraj!")

    #funckja sprawdzajaca czy uzytkownik zgadnal
    def guess(self):
        try:
            input_value = int(self.input_box.get())
            los = self.number_rand
            response = ""
            if input_value < los:
                response = "ZA MALO !"
            elif input_value>los:
                response = "ZA DUZO !"
            else:
                response = "BINGO!"
                self.guessed = True
        #Jeśli wprowadzona tresc jest niepoprawna, przypisz do odpowiedzi tresc
        except:
            response = "Niepoprawne dane"
        finally:
            self.info_box_text.set(response)
       
        
#utworzenie roota, calego okienka calej aplikacji
root = tk.Tk()
#przypisanie do zmiennej app naszej klasy i przeslanie argumentu master=root
app = Application(master=root)
#ustalenie tytulu aplikacja(naglowek u gory)
app.master.title("Zgadywanka")
#ustalenie rozmairu okienka
app.master.minsize(300,200)
#zapetlenie aplikacji, aby nie wylaczyla sie od razu
app.mainloop()










