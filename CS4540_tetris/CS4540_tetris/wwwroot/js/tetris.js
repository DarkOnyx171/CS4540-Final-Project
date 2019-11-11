//put a header here


let type = "WebGL"

if (!PIXI.utils.isWebGLSupported()) {
    type = "canvas"
}

//Create a Pixi Application
let app = new PIXI.Application({
    width: 256,
    height: 256,
    view: document.getElementById("game")
});


//Add the canvas that Pixi automatically created for you to the HTML document
document.getElementById("game_area").appendChild(app.views);
