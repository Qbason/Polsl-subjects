import tkinter      #https://docs.python.org/3/library/tkinter.html
import gamefortwo
import gameforone


#creatin class, which create Frame with labels, buttons
class Application(tkinter.Frame,tkinter.Label):
    """
    class which create frame(screen) and another elements
    """
    def go_singleplayer(self):
        self.master.destroy()
        gameforone.singleplayer()

    def go_twoplayer(self):
        self.master.destroy()
        gamefortwo.twoplayer()

    def create_widgets(self):

        #create button, which run the function go_singleplayer
        self.single = tkinter.Button(self)
        self.single["text"] = "Single Player"
        self.single["command"] = self.go_singleplayer
        self.single["fg"] = "green"
        self.single.pack(side="top", fill="x",ipadx=20,ipady=20,padx=40,pady=30)

        #create button, which run the function go_twoplayer
        self.multi = tkinter.Button(self)
        self.multi["text"] = "Two Player"
        self.multi["command"] = self.go_twoplayer
        self.multi["fg"] = "green"
        self.multi.pack(fill="x",ipadx=20,ipady=20,padx=40,pady=30)

        #create button quit
        self.quit = tkinter.Button(self)
        self.quit["command"] = self.master.destroy
        self.quit["text"] = "QUIT"
        self.quit["fg"] = "red"
        self.quit.pack(fill="x",ipadx=20,ipady=20,padx=40,pady=30)

        self.label = tkinter.Label(text="Snake Game",fg="black",bg="white")
        self.label.pack(fill="x",ipadx=20,ipady=20)


    #define the main constructor of frame
    def __init__(self,master=None):
        super().__init__(master)    # it is new method to make constructor of the parent class
        self.master = master
        self.master.minsize(400,400)
        self.master.maxsize(400,500)
        self.master.config(bg="green")
        self.master.title("Snake Game, made by Jakub Kościelny")
        self.pack()
        self.create_widgets()

        
root = tkinter.Tk()

app = Application(master = root)


app.mainloop()
































