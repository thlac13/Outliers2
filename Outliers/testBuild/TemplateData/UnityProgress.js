function UnityProgress(gameInstance, progress) {
  if (!gameInstance.Module)
    return;
  if (!gameInstance.logo) {
    gameInstance.logo = document.createElement("div");
    gameInstance.logo.className = "logo " + gameInstance.Module.splashScreenStyle;
    gameInstance.container.appendChild(gameInstance.logo);
  }
  if (!gameInstance.progress) {    
    gameInstance.progress = document.createElement("div");
    gameInstance.progress.className = "progress " + gameInstance.Module.splashScreenStyle;
    gameInstance.progress.empty = document.createElement("div");
    gameInstance.progress.empty.className = "empty";
    gameInstance.progress.appendChild(gameInstance.progress.empty);
    gameInstance.progress.full = document.createElement("div");
    gameInstance.progress.full.className = "full";
    gameInstance.progress.appendChild(gameInstance.progress.full);
    gameInstance.container.appendChild(gameInstance.progress);
  }
  gameInstance.progress.full.style.width = (100 * progress) + "%";
  gameInstance.progress.empty.style.width = (100 * (1 - progress)) + "%";
  FBInstant.setLoadingProgress(progress*100);
  if (progress == 1) {

    {FBInstant.startGameAsync()
    .then(function() {
      console.log(FBInstant.player.getName());
      console.log(FBInstant.context.getID());

      var contextId = FBInstant.context.getID();

      FBInstant.context.switchAsync(contextId).then(function() {
        FBInstant.context.getPlayersAsync()
        .then(function(players) {
        console.log(players);
   // _leaderboard.render(players);

  }); 
      });

      

  FBInstant.context
  .chooseAsync()
  .then(function() {
//    console.log(FBInstant.context.getID());


 //   var playerName = FBInstant.player.getName();
    FBInstant.updateAsync({
      action: 'CUSTOM',
      cta: 'Join the game!',
      image: 'data:image/jpeg;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAU1QTFRFNjtAQEVK////bG9zSk9T/v7+/f39/f3+9vf3O0BETlJWNzxB/Pz8d3t+TFFVzM3O1NXX7u/vUldbRElNs7W3v8HCmZyeRkpPW19j8vLy7u7vvsDC9PT1cHR3Oj9Eo6WnxsjJR0tQOD1Bj5KVgYSHTVFWtri50dLUtLa4YmZqOT5D8vPzRUpOkZOWc3Z64uPjr7Gzuru95+jpX2NnaGxwPkNHp6mrioyPlZeadXh8Q0hNPEBFyszNh4qNc3d6eHx/OD1Cw8XGXGBkfoGEra+xxcbIgoaJu72/m52ggoWIZ2tu8/P0wcLE+vr7kZSXgIOGP0NIvr/BvL6/QUZKP0RJkpWYpKaoqKqtVVldmJqdl5qcZWhstbe5bHB0bnJ1UVVZwsTF5ubnT1RYcHN3oaSm3N3e3NzdQkdLnJ+h9fX1TlNX+Pj47/DwwsPFVFhcEpC44wAAAShJREFUeNq8k0VvxDAQhZOXDS52mRnKzLRlZmZm+v/HxmnUOlFaSz3su4xm/BkGzLn4P+XimOJZyw0FKufelfbfAe89dMmBBdUZ8G1eCJMba69Al+AABOOm/7j0DDGXtQP9bXjYN2tWGQfyA1Yg1kSu95x9GKHiIOBXLcAwUD1JJSBVfUbwGGi2AIvoneK4bCblSS8b0RwwRAPbCHx52kH60K1b9zQUjQKiULbMDbulEjGha/RQQFDE0/ezW8kR3C3kOJXmFcSyrcQR7FDAi55nuGABZkT5hqpk3xughDN7FOHHHd0LLU9qtV7r7uhsuRwt6pEJJFVLN4V5CT+SErpXt81DbHautkpBeHeaqNDRqUA0Uo5GkgXGyI3xDZ/q/wJMsb7/pwADAGqZHDyWkHd1AAAAAElFTkSuQmCC',
      text: {
        default: 'Get to know some friends!',
        localizations: {
          ar_AR: 'X \u0641\u0642\u0637 \u063A\u0632\u062A ' +
          '\u0642\u0631\u064A\u0629 Y!',
          en_US: 'Get to know some friends!',
          es_LA: '\u00A1X acaba de invadir el pueblo de Y!',
        }
      },
      template: 'new_game',
      data: { myReplayData: '...' },
      strategy: 'IMMEDIATE',
      notification: 'NO_PUSH',
    }).then(function() {
      // closes the game after the update is posted.
      FBInstant.quit();
    });

  }); 
    })};

    gameInstance.logo.style.display = gameInstance.progress.style.display = "none";
  }
}
    