# Project-Java-Polsl
Rozwiązanie problemu z Algorytmionu 

ZADANIE 1 - "SIATKA TOMKA"
Tomek narysował siatkę o wymiarach n1 na n2 składającą się z kwadratów.
Następnie próbował rozmieścić na tej siatce prostokąty o wymiarach l1 na l2 w taki
sposób, aby prostokąty nie zachodziły na siebie i nie wychodziły poza siatkę, a ich boki
pokrywały się z liniami siatki. Jeśli udało się pokryć nimi całą siatkę, zapisywał liczbę
wszystkich możliwych ustawień prostokątów na siatce. Jeśli nie było takiej możliwości,
szukał takiego ustawienia, w którym liczba pozostałych kwadratów, z których złożona
jest siatka, była najmniejsza. Wtedy notował liczbę tych kwadratów.
Wejście.
Pierwszy i jedyny wiersz wejścia zawiera cztery liczby naturalne n1, n2, l1, l2 (0
< n1 ≤ n2 < 2³², 0 < l1 ≤ n1, 0 < l2 ≤ n2) oznaczające kolejno:
ilość wierszy siatki,
ilość kolumn siatki,
krótszy bok prostokąta,
drugi bok prostokąta.
Wyjście.
Pierwszy wiersz wyjścia powinien zawierać słowo:
„TAK”, jeśli można pokryć całą siatkę prostokątami,
„NIE”, jeśli nie można pokryć całej siatki prostokątami.
Drugi (ostatni) wiersz wyjścia powinien zawierać liczbę naturalną k (0 < k < 2³²),
która oznacza liczbę wszystkich możliwych ustawień prostokątów służących do pokrycia
całej siatki, jeśli w pierwszym wierszu było „TAK”,najmniejszą liczbę kwadratów
pozostałych po ustawieniu prostokątów na siatce, jeśli w pierwszym wierszu było „NIE”.
Przykład.
Dla danych wejściowych:
5 6 2 3
Poprawnym wynikiem jest:
TAK
2
Dla danych wejściowych:
12 50 10 10
Poprawnym wynikiem jest:
NIE
100
