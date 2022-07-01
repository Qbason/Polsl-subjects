import turtle   
import time
import random
import math

#project based on the turtle libray link: https://docs.python.org/3/library/turtle.html


## contains the main methods of snake and defines  the head of our snake
class Snake(turtle.Turtle):

    #consturctor, which defines the default properites
    def __init__(self):
        turtle.Turtle.__init__(self)
        self.color("black")     #the color of our head snake is black
        self.shape("circle")    #the head of our snake has a circle shape
        self.penup()            # lift up  our pen otherwise we will see the scratches 
        self.goto(0,0)          # go to cord x=0 y=0, start point
        self.speed(0)           # the speed defines how fast is anamation of line drawing and turtle turning 0 - the fastest 
        self.direction = "stop" # if any keys were not pressed then the head stay
        self.howfargo = 10      # says how much pixels it goes per one loop
        self.segments = []      #it containes the other parts of our snake besides the head
        #self.length_seg = 0     #default lenght of the snake, it is just the head
        self.not_eaten = True   # if it is True, then game  is being played
        self.score = 0          # the general points, which we can collect for  eating food
        self.shapesize(0.5,0.5,0.5) #change the default scale 
        self.type_shape ={
            "Up":"images\\up.gif",
            "Down":"images\\down.gif",
            "Left":"images\\left.gif",
            "Right":"images\\right.gif",
            "stop":"images\\up.gif"
        }
        self.shape(self.type_shape["Up"])

    #change the position x,y of our snake head
    def move(self):
        #print("I am in move function")  #debugging
        if self.direction == "Up":
            y = self.ycor()             # take the cord y of our snake  head and set to y
            self.sety(y + self.howfargo) #change the y cord by atribute  howfargo
        
        elif self.direction == "Down":
            
            y = self.ycor()             # take the cord y of our snake  head and set to y
            self.sety(y - self.howfargo) #change the y cord by atribute  howfargo

        elif self.direction == "Left":
            x = self.xcor()             # take the cord x of our snake  head and set to y
            self.setx(x - self.howfargo) #change the x cord by atribute  howfargo

        elif self.direction == "Right":
            x = self.xcor()             # take the cord x of our snake  head and set to y
            self.setx(x + self.howfargo) #change the x cord by atribute  howfargo
        self.shape(self.type_shape[self.direction])
        # and stop, but direction stop, doesnt do anything

    #returing the actual lenght of the whole snake
    def check_length(self):
        """
        return the len of segments
        """
        return len(self.segments)

    #methods that change the directions, it is prepared for the functions, that read our keys
    #before we change the direction we have to check if the snake has the segments(body) then he cannot change
    # the direction to the opposite, because he  would eat hisself :(

    def up(self):
        #print("I am in up function")#debugging
        if(self.check_length()==0 or (self.direction!="Down" and self.check_length()>0)):
            self.direction = "Up"
    
    def down(self):
        #print("I am in down function")#debugging
        
        if(self.check_length()==0 or (self.direction!="Up" and self.check_length()>0)):
            self.direction = "Down"
    
    def left(self):
        #rint("I am in left function")#debugging
        if(self.check_length()==0 or (self.direction!="Right" and self.check_length()>0)):
            self.direction = "Left"

    def right(self):
        #print("I am in right function")#debugging
        if(self.check_length()==0 or (self.direction!="Left" and self.check_length()>0)):
            self.direction = "Right"


    def add_segment(self):
        """
        it add a extra segment to the end
        """
        #gen a new object seg
        segment = Segment()
        #add it to the list segments
        if(self.color()[0]):#if exsists then ( it is special for two player mode)
            if (self.color()[0] == "blue"):# if is blue then segment is also blue
                segment.shape("images\\body_2.gif")
                
        self.segments.append(segment)


    #checking distance between the food and snake, if <20( the distance between centre of objects)
    
    def check_distance_food(self, food, snake_map):
        """
        if snake touch the food, function return 1, else 0
        """
        if self.distance(food)<10: #(20 * scale = 20 * 1/2  = 10)
            #add new object segment to our game(the body of snake)

            if(food.buff == "fast"):
                snake_map.fps = snake_map.fps*0.8
            if(food.buff == "slow"):
                snake_map.fps = snake_map.fps*1.2   
            if(food.buff == "double"):
                self.add_segment()
                self.add_segment()
                


            self.add_segment()
            self.score += food.value
            #change the x,y cords of our food
            food.teleportation( snake_map.obstacles)
            
            return 1
        return 0

    def snake_moving(self):
        """
        it moves the body of snake(segments)
        """
        
        #print("I am in snake_moving")#debugging
        if self.check_length() > 0: #if there exists at least one segment, then we have to move it
            #print("I am in snake_moving if")#debugging
            # we take the segment and move it to other segment, which is closer to the head
            for i in range(self.check_length()-1, 0, -1): # start from length-1, to 1, because stop range is not included https://docs.python.org/3/library/stdtypes.html#range
               # print("I am in snake_moving if for")#debugging
                # taking the cords x,y from the last but one
                x = self.segments[i-1].xcor()
                y = self.segments[i-1].ycor()
                # moving the segment, which is farther to the closer one
                self.segments[i].goto(x,y)

            # exception the segment 0 (segment[0]), which follows the head
            x = self.xcor()
            y = self.ycor()
            self.segments[0].goto(x,y)
        self.move()

    #method, which does not allow for go over frame
    def edge_behavior(self):
        """
        it checks, if snake hit the wall, then change cord to reverse
        """
        x = self.xcor()
        if x < - 295 or x > 295:
            self.setx(-x)

        y = self.ycor()
        if y < -295 or y> 295:
            self.sety(-y)
    
    #checking colision with self
    def check_collision_with_self(self,sleep = 1):
        """
        if snake touch the myself, return 1 else 0
        """
        for segment in self.segments:
            if self.distance(segment)<10:
                if sleep:          # on singleplayer sleep is default on
                    time.sleep(0.5)
                self.end_game()
                return 1
        return 0
        



    def check_collision_with_obstacle(self, snake_map,sleep = 1):
        """
        It checks collision with obctacles 
        """
        obs = snake_map.obstacles
        
        for o in obs:
            if self.distance(o)<15:
                if sleep:       # on singleplayer sleep is default on
                    time.sleep(0.5)
                self.end_game()
                return 1
        return 0 
        


    #ending game -> game over
    def end_game(self):
        """
        do resest_snake + goto(0,0)
        """
        self.reset_snake()  #set default snake's values 
        self.goto(0,0)      #go to center

    #set default snake's values and delete segments
    def reset_snake(self):
        """
        reset snake, score = 0 , direction = stop, delete segments
        """
        self.score = 0
        self.direction = "stop"
        for segment in self.segments:
            segment.goto(1000,1000)
        del self.segments[:]




#object food
class Food(turtle.Turtle):
    #constructor,  defines parameters, atributes, like in the class snake
    def __init__(self, type_food="normal", obstacles=0): # 0 -> if empty then avoid checking
        turtle.Turtle.__init__(self)
        self.type = {
            #type : #value,color
            "fast":[10,"red","images\\boost.gif"],
            "slow":[8,"yellow","images\\slowdown.gif"],
            "double":[20,"orange","images\\double.gif"],
            "normal":[6, "green","images\\food.gif"]
        }
        try: # if somebody do mistake and take impropertly type of food, take normal
            type_f = self.type[type_food]
        except:
            type_f = self.type["normal"]
            #print("Somebody chose bad type of food") #debugging
            #print("food change to default")

        self.color(type_f[1])
        self.shape("circle")
        self.shape(type_f[2])
        self.shapesize(0.5,0.5,0.5)
        self.penup()
        self.speed(0)
        self.teleportation(obstacles)
        #self.goto(random.randint(-100,100),random.randint(-100,100))
        self.value = type_f[0] # the score of  our food
        self.buff = type_food # if it gives any buff, additional functions like fast speed
        
    


    #change the cords of food to another
    def teleportation(self, obstacles):
        """
        Food, change the cords, but first searching new free point on the map
        """
        #random a new point for the food
        if obstacles: #if there exsist obstacles then loop will being done
            t1 = 1
        else:       # if obstacles is empty then avoid loop
            t1 = 0

        t2 = 1
        x = random.randint(-280,280)
        y = random.randint(-280,280)
        
        #find a new point to our food, but remeber about the obstacles
        while t1==1:
            
            for obs in obstacles:
                t2 = 0
                x_o = obs.xcor()
                y_o = obs.ycor()
               
                r = math.sqrt( ((x-x_o)**2) + ((y-y_o)**2) ) # just normal pitagoras
                #print(r)#debugging
                if r <15:
                    #print(r) #debugging
                    x = random.randint(-280,280)
                    y = random.randint(-280,280)
                    t2 = 1
                    # if new point is generated in the obstacle, marked it and leave loop and search again
                    break
                
            if t2==1:
                t1 = 1
                #if new point is marked then dont stop the loop
                
            elif t2==0:
                t1 = 0
                # if everything is ok, then we can leave loop
            
            
                
            


        self.goto(x,y)


class Segment(turtle.Turtle):
    #constructor,  defines parameters, atributes, like in the class snake
    def __init__(self):
        turtle.Turtle.__init__(self)
        self.color("brown")
        self.shape("circle")
        self.shape("images\\body.gif")
        self.penup()    
        self.speed(0)
        self.goto(1000,1000)    #throw this obj away, we can't see it
        self.shapesize(0.5,0.5,0.5) # resize the obj. by half

#creating class Obstacle
class Obstacle(turtle.Turtle):
    def __init__(self):
        turtle.Turtle.__init__(self)
        self.color("black")
        self.shape("circle")
        self.shape("images\\obs.gif")
        self.penup()


#creating class Map, this class will generate object and change speed of snake
class Map():
    def __init__(self):
        self.level = 1
        self.max_level = 6
        self.obstacles_properties = {
            #each level has big tuple, which contains a object properties of obstacle
            # I used tuple, because we will not change the data inside
            # (x-cord,y-cord,stretch x, stretch y, outline)
            #level 1:
            1 : (
            (0,200,1,1,1),
            (0,-200,1,1,1),
            (200,0,1,1,1),
            (-200,0,1,1,1)
            ),
             #level 2:
            2 : (
                (-50,200,1,1,1), (50,200,1,1,1),
                (-50,-200,1,1,1), (50,-200,1,1,1),
                (200,-50,1,1,1), (200,50,1,1,1),
                (-200,50,1,1,1), (-200,-50,1,1,1)
            ),

            3 : (
                (-100,100,1,1,1), (0,100,1,1,1), (100,100,1,1,1),
                (100,0,1,1,1),(-100,0,1,1,1),
                (-100,-100,1,1,1), (0,-100,1,1,1), (100,-100,1,1,1)
            ),

            4 : (
                (-150,0,1,1,1), (-110,40,1,1,1),(-70,80,1,1,1), (-30,120,1,1,1),
                (150,0,1,1,1), (110,-40,1,1,1),(70,-80,1,1,1), (30,-120,1,1,1)
            ),

            5 : (
                (-290,290,1,1,1), (-250,290,1,1,1),(-210,290,1,1,1), (-160,290,1,1,1),
                (-120,290,1,1,1), (-80,290,1,1,1),(-40,290,1,1,1), (0,290,1,1,1),
                (40,290,1,1,1), (80,290,1,1,1),(120,290,1,1,1), (160,290,1,1,1),
                (200,290,1,1,1),(240,290,1,1,1),(280,290,1,1,1),

                 (-290,-280,1,1,1), (-250,-280,1,1,1),(-210,-280,1,1,1), (-160,-280,1,1,1),
                (-120,-280,1,1,1), (-80,-280,1,1,1),(-40,-280,1,1,1), (0,-280,1,1,1),
                (40,-280,1,1,1), (80,-280,1,1,1),(120,-280,1,1,1), (160,-280,1,1,1),
                (200,-280,1,1,1),(240,-280,1,1,1),(280,-280,1,1,1)

            ),
            6:(
                (-290,290,1,1,1), (-250,290,1,1,1),(-210,290,1,1,1), (-160,290,1,1,1),
                (-120,290,1,1,1), (-80,290,1,1,1),(-40,290,1,1,1), (0,290,1,1,1),
                (40,290,1,1,1), (80,290,1,1,1),(120,290,1,1,1), (160,290,1,1,1),
                (200,290,1,1,1),(240,290,1,1,1),(280,290,1,1,1),

                 (-290,-280,1,1,1), (-250,-280,1,1,1),(-210,-280,1,1,1), (-160,-280,1,1,1),
                (-120,-280,1,1,1), (-80,-280,1,1,1),(-40,-280,1,1,1), (0,-280,1,1,1),
                (40,-280,1,1,1), (80,-280,1,1,1),(120,-280,1,1,1), (160,-280,1,1,1),
                (200,-280,1,1,1),(240,-280,1,1,1),(280,-280,1,1,1),

                (280,-250,1,1,1),(280,-210,1,1,1), (280,-160,1,1,1),
                (280,-120,1,1,1), (280,-80,1,1,1),(280,-40,1,1,1), (280,0,1,1,1),
                (280,40,1,1,1), (280,80,1,1,1),(280,120,1,1,1), (280,160,1,1,1),
                (280,200,1,1,1),(280,240,1,1,1),(280,280,1,1,1),

                (-290,-250,1,1,1),(-290,-210,1,1,1), (-290,-160,1,1,1),
                (-290,-120,1,1,1), (-290,-80,1,1,1),(-290,-40,1,1,1), (-290,0,1,1,1),
                (-290,40,1,1,1), (-290,80,1,1,1),(-290,120,1,1,1), (-290,160,1,1,1),
                (-290,200,1,1,1),(-290,240,1,1,1),(-290,280,1,1,1),
                #4
                (-150,0,1,1,1), (-110,40,1,1,1),(-70,80,1,1,1), (-30,120,1,1,1),
                (150,0,1,1,1), (110,-40,1,1,1),(70,-80,1,1,1), (30,-120,1,1,1),
                #3
                (-100,100,1,1,1), (0,100,1,1,1), (100,100,1,1,1),
                (100,0,1,1,1),(-100,0,1,1,1),
                (-100,-100,1,1,1), (0,-100,1,1,1), (100,-100,1,1,1),
                #2
                (-50,200,1,1,1), (50,200,1,1,1),
                (-50,-200,1,1,1), (50,-200,1,1,1),
                (200,-50,1,1,1), (200,50,1,1,1),
                (-200,50,1,1,1), (-200,-50,1,1,1),
                #1
                (0,200,1,1,1),
                (0,-200,1,1,1),
                (200,0,1,1,1),
                (-200,0,1,1,1)
            )

           




        }
        self.obstacles = []
        self.points = 50 # start points to pass the first level 
        self.fps = 0.15 #how fast loop will being done

    #method, that will change the value of points, which have to been reach to win one map
    def how_much_points_to_reach_next_level(self):
        self.points = self.points + self.level *10

        
    #method to generate obstacles on the field, it based on the obstacles_properties
    def generate_obstacles(self):

        if self.level<=len(self.obstacles_properties):

            lev = self.obstacles_properties[self.level]
                
            #print(lev)
            if lev:
                for obs in lev:
                    #print(obs) #debugging

                    x = obs[0]
                    y = obs[1]
                    x_s = obs[3]
                    y_s = obs[2]
                    o_l = obs[4]

                    obstacle = Obstacle()
                    obstacle.setx(x)
                    obstacle.sety(y)
                    obstacle.shapesize(x_s,y_s,o_l)
                    self.obstacles.append(obstacle)


    def delete_obstacles(self):
        for obstacle in self.obstacles:
            obstacle.goto(1000,1000)
            del obstacle


    def level_up(self):
        self.fps = 0.15 * (1-(self.level**2)/100)
        self.level += 1

        self.how_much_points_to_reach_next_level()
        self.delete_obstacles()
        self.generate_obstacles()









