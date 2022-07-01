import random

#funkcja pobiera informacje od uzytkownika i sprawdza czy jest ona liczbą
def utworz_i_validacja():
    
    x = input()

    while  not x.isdigit(): # funkcja isdigit sprawdza czy string jest liczbą np. "123" czy to liczba, jeśli tak to zwraca True
        print("Niepoprawna dana! Wpisz liczbe jescze raz")
        x = input()

    x = int(x)
    return x
    


print("Losuje liczbe z puli od 1 do 100")

#wysolowanie liczby z puli od 1 do 100
los = random.randint(1, 100)

print("Sprobuj zgadnac: Podajac liczbe")

#Wprowadzenie danych, przypisanie liczby do zmiennej x,
# program wykona sie jesli dane sa dobrze wpisane

x = utworz_i_validacja()


#Sprawdzenie warunku czy wylosowana liczba jest rowna wprowadzonej/ Checking if input "x" is equal to random number "los"
while(los!=x):

    # Kiedy wpisana wartosc jest mniejsza niz losowana/ Input-x is less than "los"
    if(x<los):
        print("Za mała liczba!")
    else:
    # Wpisana wartosc jest wieksza od losowanej/ Input-x is better than "los"

        print("Za duża liczba!")
    x = utworz_i_validacja()

#Leave the loop/ Opuszczenie petli
print("Brawo, mój przyjacielu")
