CosineGradient
==============

![gif](https://68.media.tumblr.com/5b543cb7b73fab69dddd921ac508b624/tumblr_okhnsdVnEK1qio469o1_400.gif)

*CosineGradient* is a small package for generating cosine-based gradients in Unity.

The idea of cosine-based gradients is based on [an article written by Inigo Quilez][iq]. See [the original article][iq] for further details.

[iq]: http://www.iquilezles.org/www/articles/palettes/palettes.htm

Usage
-----

A `CosineGradient` object can be created from the Create menu (Create -> Cosine Gradient).
The coefficients of CosineGradient can be interactively edited on the inspector.

There is just a single method in the `CosineGradient` class -- `Evaluate` returns a color value at a given point in a gradient.

[The editor preview shader][preview1] can be referred as an example of evaluating a gradient in a shader.
Note that the coefficient vectors (_CoeffsA/B/C/D) should be [given from a script][preview2].

[preview1]: https://github.com/keijiro/CosineGradient/blob/master/Assets/Klak/Chromatics/Editor/Preview.shader
[preview2]: https://github.com/keijiro/CosineGradient/blob/master/Assets/Klak/Chromatics/Editor/CosineGradientEditor.cs#L181
