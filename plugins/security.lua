﻿-- security.lua
-- This file is included before any chat command is executed.

if not os.execute then
	error("Do not include security.lua again, you noob.")
end

local start_time = os.clock()

--{ READONLY TABLES
local function protect_table(t)
	return setmetatable({}, {
			__index = t,
			__newindex = function(t, key, value)
				error("Table is readonly")
			end,
			__metatable = false
		})
end

for k = 1, L.online do
	N[k] = protect_table(N[k])
end

L = protect_table(L)
debug = protect_table(debug)
--} READONLY TABLES

--{ PROTECT INCLUDE FUNCTIONS
local function protect_function(name, empty_msg, check)
	local old_func = _G[name]
	assert(old_func ~= nil, "Function "..name.." is nil.")
	if check then
		check()
	end

	_G[name] = function(arg)
		assert(type(arg) == "string", "Argument #1: String expected.")
		assert(arg ~= "", empty_msg)
		if check then
			check(arg)
		end
		return old_func(arg)
	end
end

protect_function("dofile", "Invalid file name.", function(arg)
	if debug.getinfo(3).source:sub(1, 1) ~= "@" then
		assert(not string.find(arg, "\\") and not string.find(arg, "/"),
				"Blocked for security reasons.")
	end
end)

protect_function("loadfile", "Invalid file name.", function(arg)
	if debug.getinfo(3).source:sub(1, 1) ~= "@" then
		assert(not string.find(arg, "\\") and not string.find(arg, "/"),
				"Blocked for security reasons.")
	end
end)

protect_function("require", "Invalid file name.", function(arg)
	assert(debug.getinfo(3).source:sub(1, 1) == "@", "Blocked for security reasons.")
end)

protect_function("loadstring", "Empty content!", function(arg)
	assert(debug.getinfo(3).source:sub(1, 1) == "@", "Blocked for security reasons.")
end)
--} PROTECT INCLUDE FUNCTIONS

local start_time = os.clock()

--{ DISABLE EVIL FUNCTIONS
os.execute = nil
os.exit = nil

os.getenv = nil
os.remove = nil
os.rename = nil
os.setlocale = nil

coroutine = nil
module = nil
package = nil

load = nil
pcall = nil
rawget = nil
rawset = nil
rawequal = nil
xpcall = nil
unpack = nil
getfenv = nil
setfenv = nil

io.popen = nil

io.open = nil
io.write = nil
io.input = nil
io.output = nil
--} DISABLE EVIL FUNCTIONS

math.randomseed(os.clock() * 1000)

dofile("misc_helpers.lua")
dofile("complex.lua")
dofile("internetz.lua")

--{ FUNCTION LINK()
local link_list = {
	address		= "What's your address? http://www.overclockers.com.au/image2.php?pic=images/newspics/29jan16/11.jpg",
	amaze		= "Amazing! https://i.imgur.com/O8tFZ.gif",
	applause	= "*CLAP* *CLAP* *CLAP* http://i.imgur.com/YiUe1r6.gif",
	awaynick	= "Do NOT use away nicknames! http://sackheads.org/~bnaylor/spew/away_msgs.html",
	bugs		= "Bugs everywhere <.< http://d2ykiwzv4lwge4.cloudfront.net/wp-content/uploads/2014/08/31.jpg",
	busy		= "Don't interrupt me! http://i2.kym-cdn.com/photos/images/original/000/390/314/f56.gif",
	color		= "\x02\x1f\x1d\x037,5 COLOR MADNESS",
	come		= "Ok. http://i.imgur.com/Jr1dMxV.gif",
	crocs		= "http://i.imgur.com/jo2GWlj.jpg",
	developers	= "The developers! http://www.youtube.com/watch?v=KMU0tzLwhbE",
	dj			= "Every 15 minutes there are random giveaways to the people in"..
					" #dontjoinitsatrap ! (You can join it by clicking on the channel name)",
	dramatic	= "Dramatic situation! https://www.youtube.com/watch?v=y8Kyi0WNg40",
	gemini		= "Yes, we all love him, thus invite him to your channel! :) http://i.imgur.com/g4KjEJf.png",
	grammarnazi	= "Watch your grammar! http://memesvault.com/wp-content/uploads/No-Cat-Meme-26.jpg",
	high5		= "High five! https://i.imgur.com/M0bWify.gif",
	kock		= function() dofile("quotes.lua") getQuote(5) end,
	money		= "I have money. https://i.imgur.com/yLc4poE.gif",
	nope		= "NOPE! http://i.imgur.com/RMsnqCF.gif",
	nvidia		= "One of Linus' sentences: https://www.youtube.com/watch?v=iYWzMvlj2RQ",
	panda		= "Never say no to panda. https://www.youtube.com/watch?v=X21mJh6j9i4",
	["quick-look"]	= "Let's take a quick look. http://i.imgur.com/JRcwAWP.gif",
	["rizon-prizes"]= "Rizon offers many cool prizes you can win with some luck. Get the complete list with the command "..
					c("/msg GOD XDCC list").." and try your luck!",
	repost		= "REPOST ALERT! http://i.imgur.com/FHJc4aP.gif",
	semicolons	= "http://36.media.tumblr.com/tumblr_m4y213pGlc1qa0uujo1_1280.jpg",
	slash		= "How to use slash: http://users.kymp.net/feuer/etcomic/013.jpg",
	stfu		= "http://i2.kym-cdn.com/photos/images/facebook/000/919/578/c2f.jpg",
	["tl;dr"]	= "Too long; didn't read that. http://i.imgur.com/EtYq65v.gif",
	trivia		= "Genious questions. http://i.imgur.com/3Me7a5R.png",
	typing		= "See me writing da wordz! http://replygif.net/i/937.gif",
	upvote		= "Thumbs up! http://i.imgur.com/SlUuuHP.gif",
	wdtam		= "LGTM, C DIS 2: http://users.kymp.net/feuer/etcomic/050.jpg",
	win8		= "Logical way to say how Windows 8 is: http://www.dead-fish.com/wp-content/uploads/2013/06/windows-8-shit-or-good.jpg",
	win10		= "Simple showcase of Windows 10: http://esfriki.com/f/a6LXLbR_700b.jpg",
	yes			= "Yes! YES! https://i.imgur.com/tdrBo.gif",
	youtube		= "How-to-youtube: https://www.youtube.com/watch?v=dQw4w9WgXcQ",
	["yes-sure"]= "Meh, yes sure, whatever. http://replygif.net/i/247.gif",
	["("]		= ") https://xkcd.com/859/",

	["help.api"]	= "Complete Lua plugin source: https://github.com/SmallJoker/NyisBot",
	["help.ask"]	= "How to ask questions correctly: http://rurounijones.github.io/blog/2009/03/17/how-to-ask-for-help-on-irc/",
	["help.irc"]	= "A short IRC command tutorial: http://pastebin.com/raw/MB50gxk6",
	["help.lua"]	= "PIL reference: http://www.lua.org/pil/contents.html",
	["help.luacmd"]	= "Tutorial and function reference for this IRC bot: http://pastebin.com/raw/Bj15D8zs",
	["help.memo"]	= "Use "..c("/msg MemoServ SEND <user> <text>").." to send your message to <user>.",
	["help.nick"]	= "Take a moment to read this tutorial: http://pastebin.com/raw/iHMFEq41",
	["help.treeserv"]	= "List of commands: http://apeiron.no-ip.org:6112/treeserv.php?what=commands",

	["bot.leave"]	= "Kick this bot from your channel if you think it's not required anymore.",
	["bot.invite"]	= "To invite this bot into your channel, use the chat command "..c("/invite Nyisorn #channel")
}

local link_list_keys = {}
for n in pairs(link_list) do
	table.insert(link_list_keys, n)
end
table.sort(link_list_keys)

function link(text)
	if not text then
		text = "list"
	elseif type(text) ~= "string" then
		text = tostring(text)
	end

	text = string.lower(text)
	if text == "list" then
		local text = ""
		for i,v in ipairs(link_list_keys) do
			text = text.." "..v
		end
		print(L.nick ..":".. text.." )")
		return
	end

	if not link_list[text] then
		local sensivity = string.len(text) / 3
		for k,v in pairs(link_list) do
			if stringldistance(k, text) <= sensivity then
				text = k
				break
			end
		end
	end

	local entry = link_list[text]
	if not entry then
		print(L.nick ..": Link keyword not found.")
		return
	end
	if type(entry) == "function" then
		entry() -- Reference to another function
	else
		print(entry)
	end
end
--} FUNCTION LINK()

function sell(text)
	assert(type(text) == "string", "Argument #1: String expected.")

	if math.random() < 0.167 then
		print(text.." was thrown into the \x034fire\x0F. \x033Please wait...")
		return
	end
	print("Sold "..text.." for \x033$"..(math.ceil(math.random() * 30 + 1) * 50))
end

-- Russian roulette. Have fun.
function fire()
	if math.random() > 0.167 then
		print(L.nick ..": *click*")
		return
	end
	print(L.nick ..": PAM. Congratulations, you wasted your life for nothing!")
end

function help()
	print(L.nick ..": Explanation of the basic functions: https://github.com/SmallJoker/NyisBot/blob/master/help.txt")
end

local function groupAction(nick, group, func)
	assert(type(nick) == "string" and type(group) == "string", 
			"Argument #1 and/or #2: String expected.")

	local object = getUserObject(nick)
	if not object then
		print(L.nick ..": Unknown nickname.")
		return
	end
	print("(".. L.nick ..") ".. func():gsub("{n}", object.nick))
end

function adduser(nick, group)
	groupAction(nick, group, function()
		return "Added user {n} to group ".. c(group)
	end)
end

function remuser(nick, group)
	groupAction(nick, group, function()
		return "Removed user {n} from group ".. c(group)
	end)
end

function paperfold(height, thickness)
	if type(height) == "string" then
		height = fromFancy(height, 1)
	else
		assert(type(height) == "number", "Argument #1: Number expected")
	end

	if type(thickness) == "string" then
		thickness = fromFancy(thickness, 2)
	else
		if type(thickness) ~= "number" then
			thickness = 0.1E-3
		end
	end

	local step1 = height / thickness

	-- 0.1mm * 2^x = height
	-- height / thickness = 2^x -> step1 = 2^x
	-- log(step1) = log(2) * x

	local times = math.round(math.log(step1) / math.log(2))
	local real_height = thickness * 2^times
	print(L.nick..": Folding it ".. times .." times results in a "..
		toFancy(real_height, 3) .." meter high pile of paper")
end

-- For the case when there's another Lua supporting bot
function haystack(text)
	if not text then
		text = "Hello"
	else
		text = tostring(text)
	end
	local out = ""
	local cmd = false
	local depth = 0

	--		0		1		3		  7
	-- print2("print1(\"print2(\\\"print1(\\\\\\\"test\\\\\\\")\\\")")")
	-- print1("print2(\"print1(\\\"test\\\")\")")
	-- print2("print1(\"test\")")
	-- print1("test")
	while depth < 7 do
		if cmd then
			out = out.."$ print("
		else
			out = out..">src.lua print("
		end
		out = out..string.rep("\\", 2^depth - 1) .."\""
		cmd = not cmd
		depth = depth + 1
	end
	out = out ..text
	while depth > 0 do
		out = out .. string.rep("\\", 2^(depth - 1) - 1) .. "\")"
		depth = depth - 1
	end
	print(out)
end

function decide(text)
	assert(type(text) == "string", "Argument #1: String expected")
	assert(string.find(text, '?'), "No question mark found!")
	assert(string.len(text) > 8, "Too short question!")

	local rand = math.random()
	if rand < 0.35 then
		print(L.nick ..": Yes")
	elseif rand < 0.7 then
		print(L.nick ..": No")
	else
		print(L.nick ..": Maybe")
	end
end

function executionTime()
	local t = os.clock() - start_time
	print(L.nick .. ": Took " .. math.round(t * 1000, 2) .. " ms")
end

function coin(n)
	n = n or 1
	assert(type(n) == "number", "Argument #1: Number expected")
	assert(n < 1E5, "Number too large.")
	assert(n > 0, "Number too small.")

	local heads = 0
	for i = 1, n do
		if math.random() < 0.5 then
			heads = heads + 1
		end
	end
	
	print("Flipped one coin "..n.." times. It took "..
			math.round(math.random() * n / 20, 2).." minutes"..
			" to count the heads and finally got ".. heads..
			" of them. ("..  math.round(heads * 100 / n, 2) .."%)")
end