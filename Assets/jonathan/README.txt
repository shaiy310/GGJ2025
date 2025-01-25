Instructions
------------
Put `ExamplePrefab` in a scene to play the transition video with a transparent background.

Note that said prefab's `Video Player` component is set to "Loop" for testing purposes.

Also note that the video runs a little longer than expected, with a few extra bubbles on the right.

Explanation
-----------
Basically, `ChromaKey` (the material) was set via `ChromaKeyShaderGraph` to take whatever texture it receives as input, and ignore `ChromaKeyColor` using a given threshold (`Range`) and feathering (`Fuzziness`).

You can play with the above properties to customize the Chroma Key effect as needed.

`ExamplePrefab` has its material set to `ChromaKey`, and its `Video Player` component plays `TransitionVideo` onto the `ChromaKey` material using the `Material Override` mode, thereby overwriting `_BaseTexture` as it plays.