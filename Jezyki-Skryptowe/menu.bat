@echo off
setlocal enabledelayedexpansion
echo owca

:menu
echo "MENU"
echo "1. Uruchom"
echo "2. Backup"
echo "3. Informacje"
echo "4. Koniec"
set /p choice="Podaj liczbe od 1 do 4: "


if %choice% equ 4 (
	echo "Wyjscie z programu"
	pause
	exit
)
if %choice% equ 1 (
	
	

:checkarguments
	if [%1] NEQ [] (
		if [%2] NEQ [] (
			goto isexist
			
		)else (
			echo Nie podano drugiego parametru
		)
			
	)else (
		echo Nie podano pierwszego parametru
		
	)		

pause
goto end
:isexist
if exist %1 (
	if exist %2 (
		echo Dane wejściowe plik %2 
		more %2
		echo Uruchamianie %1 z parametrem %2
		python %1 %2
		echo Skrypt %1 zakonczony
		
		
			

	)else (
		echo Plik %2 nie istnieje	
	)
)else (
	echo Plik %1 nie istnieje
	
)
pause
goto end

)
if %choice% equ 2 (

	echo "Trwa tworzenie backup'a"
	if not exist Backup%date% (
		md Backup%date%
	)
	copy *.* Backup%date%
	echo "Kopia wykonana"
	pause
	goto end

)
if %choice% equ 3 (
echo Informacje o projekcie: 
echo W ramac projektu zdecydowałem się na rozwiązanie zadania 2 z Algorytmionu 2012
echo Danymi wejściowymi do programu ma być plik wej.txt
echo Wyjściowymi danymi od programu jest plik wyj.txt
echo Autor: Jakub Koscielny MS NS INF III
pause
goto end

)
echo Niepoprawny wybor, sprobuj ponownie
pause


:end
cls
goto menu




