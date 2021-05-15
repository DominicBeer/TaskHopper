# TaskHopper
A way of organising projects in Grasshopper, create a grasshopper of Trello like task cards.

Aim 1 - create a task card component with custom UI for editing task info: 
    Task owner
    Task description
    Task status - To Do, In progress, Review, Done, Expired
    Task Deadline/Done date
    Extra tags

Task card can be linked together using Grasshopper wires to show the dependency flow in the project. 
E.g a task's inputs are other tasks that this task is dependent on the results of. A tasks status 
cannot be ahead of the tasks it is dependent upon. If, due to a project change, a task is expired, all
downstream tasks are expired as well. 

Aim 2 - create a Kanban board to display the cards for their status vs either owner or tags
