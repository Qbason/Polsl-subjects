import engine
import turtle #https://docs.python.org/3/library/turtle.html
import time
import random


## creating class, which will show us a score(sum of points)
class Score(turtle.Turtle):
    """
    shows score on screen
    """
    def __init__(self):
        turtle.Turtle.__init__(self)
        self.penup()
        self.speed(0)
        self.shape("square")
        self.color("white")
        self.hideturtle()
        self.goto(0,240)
        
    #method, that will show the score, it just the another object turtle, but it writes
    def show_score(self, snake, snake_map):
        score_snake = snake.score
        score_map = snake_map.points
        level = snake_map.level
        self.clear()
        self.write("Score: {} To level up: {}".format(score_snake,score_map),
            align="center",
            font=("Arial",16,"italic")
            )
        self.goto(240,-280)
        self.write("Level {}".format(level),
            align="center",
            font=("Arial",16,"italic")
            )
        self.goto(0,240)


    #it shows, that u passed a levels
    def show_win(self):
        self.clear()
        self.write("You Won, Congratulation",
        align="center",
        font=("Courier",21,"normal"))



def singleplayer():
    """
    run singleplayer game
    """

    #Creating screen
    wn = turtle.Screen()
    width = 600
    height = 600

    wn.setup(width,height)
    wn.title("Snake one player")
    wn.bgcolor("gray")
    wn.delay(0)
    

    #setting the path to graphics of snake and food

    pic = [
       
        #food
        "images\\food.gif",
        "images\\boost.gif",
        "images\\slowdown.gif",
        "images\\double.gif",
        #snake
        "images\\body.gif",
        "images\\up.gif",
        "images\\down.gif",
        "images\\left.gif",
        "images\\right.gif",
         #obs
        "images\\obs.gif"
        
    ]


    #loading graphic to screen
    for i in pic:
        wn.register_shape(i)
    
  



    #create map
    snake_map = engine.Map()
    #create snake
    snake = engine.Snake()

    #we can decide, which food we want to see
    base_food = ["normal","fast","slow","double","double"]
    #create foods
    food = []
    
    for i in base_food:
        food.append(engine.Food(i))

    

  
    

    #add Score pic
    score = Score()

    #keys active move
    wn.listen()
    wn.onkey(snake.up,"Up")
    wn.onkey(snake.down,"Down")
    wn.onkey(snake.left,"Left")
    wn.onkey(snake.right,"Right")

  


    while snake.not_eaten:
        for foo in food:
            snake.check_distance_food(foo,snake_map)
        score.show_score(snake,snake_map)

        

        if (snake.score >=snake_map.points):
            snake.end_game()

            time.sleep(0.5)
            wn.tracer(0)
            snake_map.level_up()
            wn.tracer(1)
            

            if(snake_map.level > snake_map.max_level):
                snake.end_game()
                score.show_win()
                snake.not_eaten = False

            
            for foo in food:
                foo.teleportation(snake_map.obstacles)

            

        snake.snake_moving()
        snake.check_collision_with_self()
        snake.edge_behavior()
        snake.check_collision_with_obstacle(snake_map)
        
        
        time.sleep(snake_map.fps)
        

    wn.mainloop()

