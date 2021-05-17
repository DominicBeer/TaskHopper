# TaskHopper
A way of organising projects in Grasshopper, create a grasshopper of Trello like task cards.

Aim 1 - create a task card component with custom UI for editing task info: 
    Task owner
    Task description
    Task status - To Do, In progress, Review, Done, Expired
    Task Deadline/Done date
    Extra tags

Task card can be linked together using Grasshopper wires to show the dependency flow in the project. 
E.g a task's inputs are other tasks that this task is dependent on the results of. A tasks status cannot be ahead of the tasks it is dependent upon. If, due to a project change, a task is expired, alldownstream tasks are expired as well. 

Aim 2 - create a Kanban board to display the cards for their status vs either owner or tags

Progress Diary
15/05/21
- Have a test task card attributes working (custom component ui).
- Next up: flesh out the component to have params and solve instance. Do the data entry form on the component.

17/05/21
- Have begun to implement the the windows form to edit the task card. Currently it reads info from the card, the user can edit the data in the form, but the card does not receive the data when it is closed.
- Next up - refactor tag adding mechanism to single control
- Implement tag scraping mechanism
- Add "OpenEditForm" method to component, rather than have the functionality in "RespondToDoubleClick" 
