# RUNON-Mobile Exercise Game

In this project designed to solve the inactivity problem and to encourage people to exercise, people will be able to enjoy sports and prevent the search for space.In practice, which aims to raise awareness of people, fight against diseases and create a healthy society, the individual can follow their daily movements and increase his / her activity according to the suggestions to be given. In doing all this, the hard-earned habit can be made by having fun, not by force, as opposed to other practices, and can develop socially by communicating with other people through play.

## Methods and Work Done So Far:

### 1.Design Phase

Canvases and game screens prepared by each game are prepared. Using Unity, launcher screen, the game in and out screens are created. The design could not be made for the beginning. According to the project plan, from the acquisition of the required Assets, the design will be completed in April-May Unity. The purpose to create basic screens is to collect sensor data while at the same time controlling and testing ML algorithms in the game.

## 2.Machine Learning Phase

### 2.1 Collecting Data and Creating Train Dataset

Data collection is completed with an externally installed application. At this stage, data were collected separately for each speed stage determined and recorded in CSV format. Then R Studio was used in the whole study. The purpose of this is that R Studion is useful in data processing. All the data received were combined into tables and a function was written to normalize the data so that the average values of the activity performed in small time intervals were calculated and made suitable for classification.

### 2.2 Classification

Classification operations were made in R Studio using the created train dataset. Firstly, imputation was performed on the data and the data was reserved for use as train (70%) and test (30%). Then KNN, NAIVE BAYES, and DECISION TREE models were prepared with the train dataset. The success rate is over 70% in all models. Then prediction was made according to the models created using the test dataset. Although the most successful model in Prediction is Naive Bayes, the sensitivity value was found low. It is determined that the classifications that will best match our game are KNN and Decision Tree.With this classification process codes and detailed studies, a report was prepared with RStudio .rmd file. You can find it in Machine Learning Reports file with two different alternatives (.html, .doc). You can open the files by downloading them.

### 2.3 Classification in Unity

The research was done to classify in Unity. As a result of this research, the use of ready-made plugins was stopped to improve the performance of the game and the algorithms written were examined. A script that dynamically collects and processes data was prepared (Accelerometer.cs).
A script that can classify dynamically collected data according to the train data previously prepared in the KNN algorithm was investigated.
As a result of the research, the most appropriate classification codes were found through GitHub and all permissions and rights were obtained from the person who prepared it. With no definitive decision, the KNN script was integrated into the barber game.

github link for KNN: https: //github.com/zeved/KNN/tree/master/KNN



