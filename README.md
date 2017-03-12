CosineGradient
==============

![gif](http://i.imgur.com/NT53COT.gif)

*CosineGradient* is a small package for generating cosine-based gradients in Unity.

The idea of cosine-based gradients is based on [an article written by Íñigo Quílez][iq].
See [the original article][iq] for further details.

Usage
-----

Download and install [the unitypackage][download].

A `CosineGradient` object can be created from the Create menu (Create -> Cosine Gradient).
The coefficients of CosineGradient can be interactively edited on the inspector.

There is just a single method in the `CosineGradient` class -- `Evaluate` returns a color
value at a given point in a gradient.

[The gradient overlay shader example][overlay1] shows how to evaluate a gradient in a shader.
Note that the coefficient vectors (_CoeffsA/B/C/D) should be [given from a script][overlay2].

License
-------

[MIT](LICENSE.md)

[iq]: http://www.iquilezles.org/www/articles/palettes/palettes.htm
[download]: https://github.com/keijiro/CosineGradient/raw/master/KlakChromatics.unitypackage
[overlay1]: https://github.com/keijiro/CosineGradient/blob/master/Assets/Test/Shaders/GradientOverlay.shader#L18
[overlay2]: https://github.com/keijiro/CosineGradient/blob/master/Assets/Test/Scripts/GradientOverlay.cs#L67
