# TaskHopper
A way of organising projects in Grasshopper, create a grasshopper of Trello like task cards.

Aim 1 (Now done 😃) - create a task card component with custom UI for editing task info: 
* Task owner
* Task description
* Task status - To Do, In progress, Review, Done, Expired
* Task Deadline/Done date
* Extra tags

Task card can be linked together using Grasshopper wires to show the dependency flow in the project. 
E.g a task's inputs are other tasks that this task is dependent on the results of. A tasks status cannot be ahead of the tasks it is dependent upon. If, due to a project change, a task is expired, alldownstream tasks are expired as well. 

Aim 2 - create a Kanban board to display the cards for their status vs either owner or tags
With this comes:
* Component that automatically connects to all task cards in script
* Component that can extract data from tasks so they can be filtered before going into the kanban board.

In progress



# To Run
_Please note there is currently an issue with windows resolution scaling, I might or might not get round to fixing it_

In latest builds folder:
* Copy TaskHopper.gha into Grasshopper Libraries folder
* Copy .ghuser files into Grasshopper UserObjects folder for any card templates you want to have.

# To Use

* Double click on a card to edit the card.
* Enter data in card edit menu.
* Thats about it
