/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package projekt_algorytmion;

/**
 *
 * @author Qbason
 */
import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

public class Projekt_Algorytmion {

    /**
     * @param sciezka
     * @return
     */
    public static int counter_line(String sciezka){
        int lines = 0;
        try{
           Scanner reader = new Scanner(new File(sciezka)); 
           
           while(reader.hasNext()){
               reader.nextLine();
               lines++;
           }
           reader.close();
            return lines;
        }catch(Exception e){
            System.out.println("Nieudane wczytanie pliku");
            return -1;
        }
       
       
    }
    
    public static int find_last_less_than_max_index(double [][]tablica, double max){
        
        
        for(int i=0;i<tablica.length;i++){
            if(tablica[i][0]>max){
                
                return i-1;
            }
        }      
        return tablica.length-1;//the last good option
        
        
    }
    
    public static void show_array(double [][]tablica){
        
        for(int i = 0;i<tablica.length;i++){
            
            System.out.println("Index: "+(int)tablica[i][1]+" liczba: "+tablica[i][0]);
            
        }
        
    }
    
    
    
    public static boolean read_lines_to_array(double [][]numbers_array){
        File plik = new File("wej.txt");
        String tekst;
        int i = 0;
        
        try{
            Scanner odczyt = new Scanner(plik);
            odczyt.nextLine();//avoid the first line with money
            while(odczyt.hasNextLine()){
                    tekst = odczyt.next();                          //Liczba poprawna
                    numbers_array[i][0] = Double.parseDouble(tekst); // przypisanie liczby
                    numbers_array[i][1] = i+1;                      //przypisanie odpowiedniego indexu
                    odczyt.nextLine();                              //przejście do nowej lini
                    i++;
                    
            }
            odczyt.close();
            return true; //everything is fine
        }catch(Exception e){
                
            System.out.println("Nieprawidlowe dane!"+e);
            return false; // smth went wrong
        }
        
    }
     public static double read_max_money(double [][]numbers_array){
        File plik = new File("wej.txt");

        try{
            Scanner odczyt = new Scanner(plik);
            double max_money = Double.parseDouble(odczyt.nextLine());
            odczyt.close();
            return max_money; //everything is fine
        }catch(Exception e){
            //System.out.println("Nieprawidlowe dane! "+e);
            return -1; // smth went wrong
        }
        
    }
     
     public static void find_all_possible_solutions(double [][]tablica, double max_money, int last_good_index){
         
         for(int i =1;i<=last_good_index;i++){
             
             
             find_solutions_from_one_number(tablica, i, max_money);
             
             
         }
         
         
         
         
     }
     
     
     
     
     public static void find_solutions_from_one_number(double [][]tablica,int last_index, double max_money){
     
         
         double suma;
         String sposob;
         int gdzie_dobry_ostatni_index=last_index;
         boolean end_this_step=false;
         
         
         while(end_this_step==false){
             
            
             suma = 0.0;
             sposob = ""+(int)(tablica[last_index][1])+" ";
             suma = tablica[last_index][0];
             
        
            for(int i = gdzie_dobry_ostatni_index-1; 0<=i;i--){
 
                    
                    
                    if(tablica[i][0]+suma<max_money){
                        //System.out.println(gdzie_dobry_ostatni_index);
                       // System.out.println(suma);
                       if(suma == tablica[last_index][0]){
                            gdzie_dobry_ostatni_index = i;
                        }
                       
                        suma = suma+tablica[i][0];
                        sposob = sposob +(int)(tablica[i][1])+" ";
                        
                        
                    }
                    else if( tablica[i][0]+suma==max_money)
                    {
                       
                        //sposob = sposob +(int)(tablica[i][1])+" ";
                        
                        System.out.println("Indeksy: "+sposob +(int)(tablica[i][1])+" ");
                        if(gdzie_dobry_ostatni_index==last_index)
                        {
                            gdzie_dobry_ostatni_index= i;
                        }
                        
                        //break; 

                    }else if(i==0 && tablica[i][0]+suma>max_money && suma == tablica[last_index][0]){
                        //It goes to the end and didnt find anything
                        end_this_step=true;
                        
                    }
                
            }
            //System.out.println(gdzie_dobry_ostatni_index);
            if(gdzie_dobry_ostatni_index==0){
                end_this_step=true;
            }
            
        }
         
         
         
     }
    
    
    public static void main(String[] args) throws FileNotFoundException {
        
        int liczba_lin = counter_line("wej.txt");
        double [][]numbers_array = new double[liczba_lin-1][2];
        int last_good_index;
        int [][]possible_solutions;
        
        //read, how much money do we have
        double max_money = read_max_money(numbers_array);
        //reading lines
        if(max_money != -1){
            if(read_lines_to_array(numbers_array)){
            
                show_array(numbers_array);
                System.out.println("Sortowanie"); 
                Arrays.sort(numbers_array, (a,b)->Double.compare(a[0],b[0]));
                show_array(numbers_array);
                last_good_index = find_last_less_than_max_index(numbers_array, max_money);
                System.out.println(last_good_index);
                if(last_good_index>=0){
                    find_all_possible_solutions(numbers_array, max_money, last_good_index);
                }
                
            }else{
                System.out.println("Nieprawidłowo podane dane");
            }
        }else{
            System.out.println("Brak mozliwosci odczytu pliku");//if we cannot read the first line 
        }
        
        // now we have range 0 to last_good_index included where we see good combination

        
        
        
    }
    
}
