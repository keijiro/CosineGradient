CosineGradient
==============

![gif](https://i.imgur.com/z0KYTCt.gif)

**CosineGradient** is a Unity package for generating cosine-based gradients.

The idea of cosine-based gradients is based on an article written by Íñigo
Quílez. See [the original article] for further details.

[the original article]:
  http://www.iquilezles.org/www/articles/palettes/palettes.htm

Installation
------------

This package uses the [scoped registry] feature to resolve package
dependencies. Please add the following sections to the manifest file
(Packages/manifest.json).

[scoped registry]: https://docs.unity3d.com/Manual/upm-scoped.html

To the `scopedRegistries` section:

```
{
  "name": "Keijiro",
  "url": "https://registry.npmjs.com",
  "scopes": [ "jp.keijiro" ]
}
```

To the `dependencies` section:

```
"jp.keijiro.klak.cosinegradient": "1.1.0"
```

After changes, the manifest file should look like below:

```
{
  "scopedRegistries": [
    {
      "name": "Keijiro",
      "url": "https://registry.npmjs.com",
      "scopes": [ "jp.keijiro" ]
    }
  ],
  "dependencies": {
    "jp.keijiro.klak.cosinegradient": "1.1.0",
    ...
```
