# Manual Music Score Classifier

This repository contains a small WPF application that is used for manually classifying small images into the categories "Scores" or "Other" and automatically shows you the next image after a user classification.
It is part of a set of three tools:

* **Model Trainer**: https://github.com/apacha/MusicScoreClassifier for the training of a classifier that uses Deep Learning to train a model to automatically classify images into scores or something else.
* **Mobile App**: https://github.com/apacha/MobileMusicScoreClassifier for the mobile Android application that uses a trained model to perform real-time classification on a mobile device.
* **Manual Classifier**: https://github.com/apacha/ManualMusicScoreClassifier for a small C#/WPF application that can be used manually classify images, used during evaluation.

|Model Trainer|Mobile App|Manual Classifier|
|:----:|:-----:|:-----:|
|[![Build Status](https://travis-ci.org/apacha/MusicScoreClassifier.svg?branch=master)](https://travis-ci.org/apacha/MusicScoreClassifier)|[![Build Status](https://travis-ci.org/apacha/MobileMusicScoreClassifier.svg?branch=master)](https://travis-ci.org/apacha/MobileMusicScoreClassifier)|[![Build status](https://ci.appveyor.com/api/projects/status/4715vyioa98eje0k?svg=true)](https://ci.appveyor.com/project/apacha/manualmusicscoreclassifier)|


# Building the application
This application is a C#/Wpf application that requires Visual Studio to build.

## Authors
Alexander Pacha, TU Wien

## License

Published under MIT License,

Copyright (c) 2017 Alexander Pacha

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.