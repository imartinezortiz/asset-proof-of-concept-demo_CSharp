------
Asset Manager Demo
• This demo creates 5 assets, two 'Hello World' Assets, two Logger Assets and a DialogueAsset.
•
• It demonstrates how Assets can use the Asset Manager singleton to register themselves (and receive a unique id)
•
• Show how Asset_1 can invoke the log method of all (ie both) Logger Assets
• Show how the log method of one of the Logger Assets (Logger_3) can be tied to a function in this demo (could also be a logging facility of a game engine)
• Show how the same logging to all (ie both) Logger Assets now is different (Logger_2 logs to the Console and Logger_3 presents an Alert)
•
• Show that Bridges can be used to alter default behavior and are used to call game engine code from withing an asset.
•
• Show that the class and id properties of an Asset are read-only (getter only/private setter)
•
• Use a ported pubsubz event system in both EventManager and Asset.
•
• Check the Console for the output of this demo.
• 
• DialogueAsset wraps dialogue.js, a simple dialogue system.
• 
• Bridges are implemented by polymorphism using special purpose interfaces like ILogger and IDataStorage.
• An Asset first examines it's private bridge, then the asset manager's one and if none these implement the
• desired interface it can expose default behavior.

------
Software Patterns & Techniques used:
• Singleton (AssetManager)
• Event Subscriber/Subscription
• Self registration (Assets)
• Class
• Interfaces
• Base class (Asset).
• Polymorphism using interfaces (bridges)

------
3rd Party code and code fragments ported/used:
http://addyosmani.com/resources/essentialjsdesignpatterns/book/
https://github.com/addyosmani/pubsubz
https://github.com/scottbw/dialoguejs/

------
Some of above code is extended/adapted: 
	pubsubz:		ported and added define method allow creation of events without directly subscribing to it.
	dialogjs:		ported and added logging/
