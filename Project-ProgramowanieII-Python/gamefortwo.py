import engine
import turtle   #https://docs.python.org/3/library/turtle.html
import time
import random


## creating class, which will show us a score(sum of points)
class Score(turtle.Turtle):
    def __init__(self):
        turtle.Turtle.__init__(self)
        self.penup()
        self.speed(0)
        self.shape("square")
        self.color("white")
        self.hideturtle()
        self.goto(0,240)
        
    #it shows the points of the snakes
    def show_score(self, snake, snake2, snake_map, points):
        score_snake = snake.score
        score_snake2 = snake2.score

        self.clear()
        self.write( " Get {} points to win".format(points), align="center", font=("Arial",16,"normal") )
        self.goto(0,-260)
        self.write( "Score Snake 1: {} | Score Snake 2: {}".format(score_snake,score_snake2), 
        align="center",
        font=("Arial",16,"normal") 
        )
        self.goto(0,260)

    #it shows, which snakes won
    def show_win(self,snake):
        name = snake.name
        self.clear()
        self.write( "Snake {} WON!".format(name),
        align="center",
        font=("Courier",21,"normal") 
        )



def twoplayer():
    """
    run twoplayer game
    """

    def check_snakes(snake1,snake2):
        # creating variable for, who win if the same snakes will hit each other in head
        los = random.randint(0,1)

        for segment in snake2.segments:
            if snake1.distance(segment)<10:
                snake1.end_game()
                #print("Head of snake 1 hit the body of snake 2") #info for me #debugging

        for segment in snake1.segments:
            if snake2.distance(segment)<10:
                snake2.end_game()
               #print("Head of snake 2 hit the body of snake 1")   #info for me #debugging

        if snake1.distance(snake2)<10:
            if los == 1:
                snake1.end_game()
            else:
                snake2.end_game()

    # set the points, when we win the game
    value_to_win=200
    def check_who_won(snake1, snake2):
        if snake1.score>=value_to_win:
            score.show_win(snake1)
            return 1
        elif snake2.score>=value_to_win:
            score.show_win(snake2)
            return 1
        return 0

    #Creating screen
    wn = turtle.Screen()
    width = 600
    height = 600
    wn.setup(width,height)
    wn.title("Snake two player")
    wn.bgcolor("gray")
    wn.delay(0)
    wn.tracer(0)

     #setting the path to graphics of snake and food

    pic = [
        #food
        "images\\food.gif",
        "images\\boost.gif",
        "images\\slowdown.gif",
        "images\\double.gif",
        #snake
        "images\\body.gif",
        "images\\body_2.gif",
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
    snake_map.level=6
    snake_map.generate_obstacles()
    
    #create snake
    snake = engine.Snake()
    snake.name = "GREEN!"
    snake.color("green")
    snake.goto(30,30)

    snake2 = engine.Snake()
    snake2.name = "BLUE!"
    snake2.color("blue") # if blue then his segment is blue, this is reserved only for blue
    snake2.goto(-30,-30)

    wn.tracer(1)


    #we can decide, which food we want to see
    base_food = ["normal","fast","slow","double","double","normal","normal","normal"]
    #create foods
    food = []
    for i in base_food:
        food.append(engine.Food(i, snake_map.obstacles))

  


    #add Score pic
    score = Score()

    # keys active move 
    #P1
    wn.listen()
    wn.onkey(snake.up,"Up")
    wn.onkey(snake.down,"Down")
    wn.onkey(snake.left,"Left")
    wn.onkey(snake.right,"Right")
    #P2
    wn.listen()
    wn.onkey(snake2.up,"w")
    wn.onkey(snake2.down,"s")
    wn.onkey(snake2.left,"a")
    wn.onkey(snake2.right,"d")



    while snake.not_eaten:
        for foo in food:
            snake.check_distance_food(foo,snake_map)
            snake2.check_distance_food(foo,snake_map)

        score.show_score(snake,snake2, snake_map, value_to_win)
        if check_who_won(snake,snake2):
            break
        

        snake.snake_moving()
        snake2.snake_moving()

        snake.check_collision_with_self(sleep=0)
        snake2.check_collision_with_self(sleep=0)

        snake.edge_behavior()
        snake2.edge_behavior()


        snake.check_collision_with_obstacle(snake_map,sleep=0)
        snake2.check_collision_with_obstacle(snake_map,sleep=0)
        
        check_snakes(snake,snake2)
        
        time.sleep(snake_map.fps)
        

    wn.mainloop()

