[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fcodecov%2Fheroku-buildpack-Cpp.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Fcodecov%2Fheroku-buildpack-Cpp?ref=badge_shield)

heroku-buildpack-Cpp
====================

Buildpack for C++

Use C++-Buildpack if you need Heroku to execute a C++ application.

Usage

NOTE: The C++ app being deployed with this buildpack needs a makefile that will specify the build rules 
of the project.


$> heroku create myapp -s cedar 

$> heroku config:add BUILDPACK_URL=https://github.com/ahanjura/heroku-buildpack-Cpp.git


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fcodecov%2Fheroku-buildpack-Cpp.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Fcodecov%2Fheroku-buildpack-Cpp?ref=badge_large)