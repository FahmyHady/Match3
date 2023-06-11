# Match3

***Hello***

I started this task with 1 hours and 30 minutes. 

I wanted to do everything myself, the classes you see are all written by me. 

The EventManager and Utilities class are pre-written, I use them in several projects, what I included here are the parts I needed.

I did not finish the task in time. 

The grid is generated at any size, length does not have to equal width, which I think would add to gameplay.

Each tile knows its neighbours, moving/checking for similar ones is a simple matter of traversing. 

I would use EventManager and Observer pattern to do almost everything in the game with the exception of the communication between GridBrain and Tiles. I'd use the command pattern for performance. 

This would allow us to scale the game bigger if all the commands come from the GridBrain and Tiles do not have to decide everything for themselves.

For the procedural generation conditions I'd have used a sort of Wave function collapse. 


In my opinion the only way to fit Task 3 in under 3 hours is to get a pre-made game and use it. The task is just too big for 3 hours. But I don't like get existing games and modify them. 


SFX and VFX are not a topic of conversation, they are super easy to add and I find including them as a bonus is a big waste of time, specially in a task this big.


Hope someone reads this and have a nice day.
