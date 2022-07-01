#include <stdio.h>
#include <stdlib.h>
#include <conio.h>


//Program wpisuje "TAK" do pliku wynik5.txt jesli sudoku jest dobrze zbudowane
//Program zapisuje "NIE" do pliku wynik5.txt jesli sudoku jest zle zbudowane



void make_zero(int* tab, int len) {

	for (int i = 0; i < len; i++)
	{
		tab[i] = 0;
	}
	return;
}


//5.1
int read_file(FILE *fptr, int**tab) {
	char c;// znak
	int ro = 0, co = 0; // row , columns
	int counter = 0; // amount of numbers
	int num = 0; // number from the file
	int c_n = 0; // it saves information, that char before is a space
	

	while ( ((c = fgetc(fptr)) != EOF) && (ro < 9) && (co < 9) ) // rows and cols less than 9 (9x9sudoku!)
	{
		
		//printf("%c", c); -> checking, if fgetc works properly 
		if ((c == '\n')) {
			ro++;// char of new line
			co = 0; //reset column
			c_n = 0; //we found the new line char
			//printf("\n", co);
		}
		else if (c == ' ') {
			if ((c_n) == 0) {
				return -2;// double space or sth like that
			}
			c_n = 0; // we found the space
		}
		else if ((c != ' ') )
		{
					if ((c_n) == 1) {
						return -2;// double number it means too big number or improperly char for example ',','.'
					}
			
					// num = c[0]-'0'; another way to change char to number
					num = atoi(&c);// changing char to number
					if (num == 0) {
						//printf("c =%c , num =%d", c, num);
						return 0;
					}
					tab[ro][co] = num; 
					co++;// increase column by 1
					//printf("%d\t", co);
					counter++; // amount of  found numbers increase by 1
					if (counter == 82) 
					{
						return -1; // if it finds 82 numbers then we have too much numbers
					}
					c_n = 1; // it remebers, that it met number
		
		}
	
		
	}
	if (counter != 81) {
		return -3; // if we find less than 81 number than we cannot do rest
	}

	return 1;

}

/*
void r_analzyer_e(int *r) {

	if (*r == 0) {
		printf("Znaleziono liczbe 0 lub niepoprawny znak w wczytanym pliku.");
	}
	if (*r == -1) {
		printf("Pliku znajduje sie wiecej niz 81 liczb");
	}
	if (*r == -2) {
		printf("Blad wprowadzonych danych w pliku dane5.txt\n");
	}
	else if (*r == -3) {
		printf("Nieprawidlowa ilosc liczb w wczytanym pliku.");
	}
	
	
}
*/

int check_repeating(int* tab, int rep) {

	for (int i = 0; i < 9; i++) {
		if (tab[i] != rep) {
			//printf("Problem z liczba %d \n", (i+1));
			return 0;
		}

	}
	return 1;
}


// 5.2
int check_rows_columns(int** tab) {

	int t_tab_r[9];
	int t_tab_c[9];


	make_zero(t_tab_r, 9);
	make_zero(t_tab_c, 9);

	
	/*
	 tab[0] counts 1
	 tab[1] counts 2
	*/
	for (int j = 0; j < 9; j++) {
		for (int i = 0; i < 9; i++) {
			
			t_tab_r[tab[j][i]-1]++; // checking tabs
			t_tab_c[tab[i][j]-1]++; // checking tabs

		}
		if (!check_repeating(t_tab_r, 1)) {
			//printf("Znalazlem blad!!wiersz");
			return 0;
		}
		make_zero(t_tab_r, 9);
		if (!check_repeating(t_tab_c, 1)) {
			//printf("Znalazlem blad!!columna ?? %d \n",j);
			return 0;
		}
		make_zero(t_tab_c, 9);

	}

	return 1;

}

int check_squares(int** tab) {

	// 0 if sth bad
	// 1 if sth good
	int k = 0;
	int t_tab[9];
	make_zero(t_tab, 9);

	for (int z = 0; z < 3; z++) {

		for (int j = 0; j < 9; j++) 
		{
			

			for (int i = k; i < (k+3); i++) 
			{
				t_tab[tab[j][i] - 1]++;
			}
			if ((j % 3) == 2)  {
				if (!check_repeating(t_tab, 1)) {
					//printf("FATAL ERROR %d %d %d", j,k,z);
					return 0;
				}
				make_zero(t_tab, 9);
			}
			

		}
		k = k + 3;
		
	}

	return 1;

}

void write_result_to_file(int *result) {

	FILE* fptr;
	fopen_s(&fptr, "wynik5.txt", "w");

	if (fptr != NULL) {

		if (*result) {
			fputs("TAK", fptr);
		}
		else {
			fputs("NIE", fptr);
		}
	}

	if (fptr != NULL) {
		fclose(fptr); 
	}

	return;

}



int main()
{
	FILE* fptr; //file pointer
	int r = 1;	//result defined as true
	int w = 1;
	
	int **tab = (int**)malloc(sizeof(int*) * 9); // 9 array of sudoku

	//printf("%d", sizeof(int));
	//allocate the memory for 9 x 9 
	if (tab){
		
		for (int i=0; i < 9; i++) {

			if (tab[i]) {
				tab[i] = (int*)malloc(sizeof(int)*9);//9 
			}
		}
		
	}

	fopen_s(&fptr,"dane5.txt", "r");
	
	if (fptr != NULL) {
		
		r = read_file(fptr, tab); // Result of sudoku, which has been read
		
		if(r==1) {
			//printf("\nWczytano prawidłowo sudoku\n");
			
			// 5.2

			if (!check_rows_columns(tab)) {
				//printf("Nieprawidlowo rozwiazane sudoku!Warunek 2 ");
				w = 0;
				
			}
			// 5.3
			else if (!check_squares(tab)) {
				//printf("Nieprawidlowo rozwiazane sudoku! Warunek 3");
				w = 0;
			}

		}
		else {
			//r_analzyer_e(&r);
			w = 0;
		}
		

	}
	else {
		//printf("Niepoprawna sciezka do pliku");
		w = 0;
	}





	if (fptr != NULL) {
		fclose(fptr); // close file
	}

	

	free(tab); // clean array of numbers


	//5.4
	write_result_to_file(&w);
	return 0;


}

