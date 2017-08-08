# About

Project Hero is a visual build status tool for Visual Studio 2017 that transforms the boring output window into a user friendly display on build status. It was designed as a pet project after frustration with the inadequate **Output Window** that comes as part of the Visual Studio IDE. I set out on a journey to understand Visual Studio Internals and a couple weeks later version one of this project came to life supporting Visual Studio 2010, 2012 and 2013. This plugin has mostly been confined to private usage at my workplace but now after converting it to support Visual Studio 2017 I am making the new code available!

Many people probably have frustration understanding Visual Studio Internals and it does require a bit of a learning curve to grasp. Fortunately, I have provided a good amount of comments, sometimes quick and dirty solutions to problems that arise out of sheer ignorance but overall a fast, stable and extensible plugin! It's my first attempt at Visual Studio Plugins and I hope you find my source code informative and helpful in your journey developing extensions for the Visual Studio Platform.

# Screenshot
![alt text](https://redorc.github.io/ProjectHero2/1.png "Project Hero 2 loaded in Visual Studio 2017 Enterprise")

# Supported Products

Currently I added support for the following editions of the IDE:

* Visual Studio Enterprise 2017
* Visual Studio Professional 2017
* Visual Studio Community 2017

# Known Issues

* There is a slight problem with the paint code responsible for displaying items in the list view grid. This is due to the fact that I'm using owner drawn list view items which requires you to handle the drawing of each row and its contents. If you hover your mouse over an item and leave it there, sometimes the item disappears until you move your mouse off or mouse move around it to cause an invalidation of the control (which repaints the surface). This can be fixed but due to a lack of time and other projects I have chosen to leave it as-is since it's of low annoyance to me and colleagues. If this issue bothers you and you would like it resolved, then please feel free to raise an issue.

* When launching the Tool Window for Project Hero the first time around the columns will not be properly adjusted and will require that you adjust it to your liking. However, once the column sizes are set the settings are saved automatically for you.

* The user control is based off of Windows Forms which was adequate for Visual Studio 2010 - 2013. With the introduction of Visual Studio 2017 it is **highly recommended** that a WPF Native Control is utilized for best performance. I'm not the biggest fan of WPF and have therefore not bothered in the conversion of the user control from Win Form to WPF. It is simply embedded in the body of a wrapper WPF Control so that it may be hosted in a tool window to be displayed to the user. If you would like take this on as an exercise then feel free to upgrade it to a WPF Control :)

# Setup Instructions

Visit your extension manager in Visual Studio and search for "Project Hero 2". Install it and then be sure to restart all instances of Visual Studio 2017 if required. Once re-opened you should see the Project Hero menu at the top of your IDE menu bar. Click on it and select **"Show Project Hero"** and you should see the tool window created. Dock it where ever it is convenient and then start loading up your projects! 

# Final Thoughts

If you have any questions on this plugin or would like some tips on porting this to another IDE or platform please feel free to give me a buzz.
