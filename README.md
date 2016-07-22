# _Band Tracker_

#### _c# Exercise for Epicodus, 07.22.2016_

#### By _**Bo Zhao**_

## Description

_A program to track bands and the venues where they've played concerts._

## Setup/Installation Requirements

* _Set up git project for Windows_
* _Clone this repository_
* _Execute band_tracker.sql and band_tracker_test.sql in SSMS_
* _Set up dependencies in PowerShell by typing "dnu restore"_
* _Start the kestrel web server by typing "dnx kestrel"_
* _Open your web browser of choice to localhost:5004_

## Details of Creating Database
* _CREATE DATABASE band_tracker;_
* _GO_
* _USE band_tracker;_
* _GO_
* _CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));_
* _CREATE TABLE venues (id INT IDENTITY(1,1), place VARCHAR(255);_
* _GO_

## Technologies Used

* _c#_
* _Nancy_
* _Razor_
* _html_
* _SSMS_

### License

*This software is licensed under the MIT License*

Copyright (c) 2016 **_Bo Zhao_**
