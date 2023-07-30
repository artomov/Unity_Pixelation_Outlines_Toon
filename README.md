# Unity: Pixelation, Outlines, Toon

*Unity version: 2022.3.1f1 (LTS)*
*URP version: 14.0.8*

This mini-project contains my implementation of pixelation post processing as well as outlines post processing. Both implemented as "Full Screen Pass Render Feature" using full sceen materials. Besides it contains my toon shader. The project is just an example of how to use the implemented features.

## Pixelation 
*(this is a full screen render feature, so you can enable/disable it in the renderer data asset)*
This one is preaty basic pixelation effect that does multiply->floor->divide of the screen position node, which is then used as a UV coordinate to the URP Sample Buffer node. This one is inspired by [Ben Cloward's video](https://www.youtube.com/watch?v=x95xhWCxBb4). Although as parameter in full screen material I use the target resolution's height (no need to worry about the width, it is being calculated depending on the aspect ratio of your screen). This means that if you set this parameter for example to 3, your image will be 3 giant pixels high.

## Outlines
*(this is a full screen render feature, so you can enable/disable it in the renderer data asset)*
This one is after the [Daniel Ilett's post](https://danielilett.com/2023-03-21-tut7-1-fullscreen-outlines/). Nothing else to add really. Although it has  

## Toon
Well, main difficulty while making a toon shader (in ShaderGraph I meen) is to get information about lighting. At least at the current version (see the top portion of this readme file) the ShaderGraph doesn't have a dedicated node for lighting, so this info hhas to come from custom HLSL function. To do so I followed [the turorial by NedMakesGames](https://nedmakesgames.medium.com/creating-custom-lighting-in-unitys-shader-graph-with-universal-render-pipeline-5ad442c27276), however I've simplified his function quite a bit. In fact, I ended up having 2 separate functions. One returns information about the main light, and the other returns similar information of the i-th additional light (i being a parameter passed to the function). The information returned is: light color, light direction and combined light attenuation (distance attenuation * shadow attenuation). The rest of the logic of the toon shader happens in the ShaderGraph. Note that in the current wersion of the graph I have 5 nodes dedicated to additional lights, which means that it supports up to 5 additional light sources per object (can be easily increased by adding more nodes). Also usually people use ramp textures for defining the light levels in a toon shader, but I didn't want to use a separate image edditor to create new textures every time I wanna try something new. Instead I've created a Scriptable Object that generates such textures from Gradients (I use 2 ramp textures, one for main light and second for additional ones). This scriptable objects requires an external script triggering the generation (just look at the code, it is pretty straightforward). Probably it would be beter to create a custom edditor which would allow for a button for texture generation instead of having an additional script constantly listening for a scriptable objec... But I am too lazy for that!
