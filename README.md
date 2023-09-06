# Kolibri Task by David Poussif

## Approach

The main approach was to add functionality changing the current logic as little as possible, reusing the gameobjects as much as possible instead of creating new ones and maintaining the architectural style. 
The implementation uses the same mine elements, then saves the values when mineswitching and loads the new mine's values. 

### Changes

- `GameProgress` saves and loads Game Progress
- Area Models and Worker controllers have a Method to handle the Level change
- `HudButtonController` handles the UI buttons animations
- `MineSwitchController` Handles the Mine election panel.

## Expandability

- More Mines: The implementation can handle more mines. The mine buttons just need to be added to MineSwitchView.
- Button Animation: The buttons have a simple script that handles the animation and called when the button has been released, it can be reused for any UI Buttons.

## Performance

- Since the Mine's gameObjects are reused, not destroyed and created new ones, there is lower risk of memory leaks.
- Animations are handled with code rather than a plug-in or SDK.
- Observables are added to Composite Disposable.

## Possible Next Steps and Improvements

Depending on the Design needs and direction of the project, next steps could be:
- Save StashAmount when switching mines 
- Make a Mine class to save mine parameters
- Adding more mines can easily be done by adding more buttons to MineSwitchView
- Make a config for Mines and possibly add Mines names and MineID
- Separate GameProgess's saving and loading functionalities into other scripts

## Testing

The Initial Amount of Money for new mines can be set in the Game Config in order to facilitate testing.