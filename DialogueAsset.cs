// <copyright file="dialogueasset.cs" company="RAGE">
// Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>10-4-2015</date>
// <summary>Implements the DialogueAsset class</summary>
namespace asset_proof_of_concept_demo_CSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DialogueAsset : Asset
    {
        public struct Dialogue
        {
            public String id;
            public String actor;
            public String text;
            public Int32 next;
            public List<Int32> responses;

            public Boolean isResponse
            {
                get
                {
                    Int32 tmp;
                    return Int32.TryParse(id, out tmp);
                }
            }
        }

        public struct State
        {
            public String actor;
            public String player;
            public Int32 state;
        }

        //! TODO Implement the banana code.
        //


        List<Dialogue> Dialogues = new List<Dialogue>();
        List<State> States = new List<State>();

        private Int32 FindStateIndex(String actor, String player)
        {
            Int32 index = States.FindIndex(p => p.actor.Equals(actor) && p.player.Equals(player));

            if (index == -1)
            {
                States.Add(new State()
                {
                    actor = actor,
                    player = player,
                    state = 0
                });
            }

            return (index != -1) ? States[index].state : 0;
        }

        private void UpdateState(String actor, String player, Int32 state)
        {
            Int32 index = States.FindIndex(p => p.actor.Equals(actor) && p.player.Equals(player));

            if (index == -1)
            {
                // New State
                States.Add(new State()
                {
                    actor = actor,
                    player = player,
                    state = state
                });
            }
            else
            {
                // Update State
                States[index] = new State()
                {
                    actor = actor,
                    player = player,
                    state = state
                };
            }
        }

        public Dialogue interact(String actor, String player, Int32 response)
        {
            return interact(actor, player, response.ToString());
        }

        /// <summary>
        /// Interacts.
        /// </summary>
        ///
        /// <param name="actor">    The actor. </param>
        /// <param name="player">   The player. </param>
        /// <param name="response"> The response. </param>
        public Dialogue interact(String actor, String player, String response = null)
        {
            Int32 state = FindStateIndex(actor, player);
            //Int32 state = index != -1 ? States[index].state : 0;

            Dialogue dialogue;

            // Dialogue responseDialog = Dialogues.First(p => p.actor.Equals(actor) && p.id.Equals(response));

            Int32 tmp;
            Boolean numeric = Int32.TryParse(response, out tmp);

            Int32 ndx = Dialogues.FindIndex(p => p.actor.Equals(actor) && p.id.Equals(response));

            if (ndx != -1)
            {
                Dialogue response_dialogue = Dialogues.Find(p => p.actor.Equals(actor) && p.id.Equals(response));

                if (numeric)
                {
                    // If its an integer response, move the dialogue state as this is a 
                    // response choice 
                    // 
                    if (response_dialogue.isResponse)
                    {
                        Console.WriteLine("  << {0}.", response_dialogue.id);
                        state = response_dialogue.next;
                        UpdateState(actor, player, state);
                    }

                    dialogue = Dialogues.First(p => p.actor.Equals(actor) && p.id.Equals(state.ToString()));
                }
                else
                {
                    // 
                    // ... otherwise this was a "what about the [item]" type of choice 
                    // so we return the dialogue but don't modify the state 
                    // 
                    dialogue = response_dialogue;
                }
            }
            else
            {
                dialogue = Dialogues.First(p => p.actor.Equals(actor) && p.id.Equals(state.ToString()));
            }

            //126     // Process responses 
            //127     // 
            //128     var responses = new Array(); 
            //129     if (dialogue.responses) { 
            //130         for (r in dialogue.responses) { 
            //131             var response = Dialogue.__getDialogue(actor, dialogue.responses[r]); 
            //132             responses.push({ id: response.id, text: response.text }); 
            //133         } 
            //134     } 
            //135 

            //136     var dialogue_processed = {}; 
            //137     dialogue_processed.text = dialogue.text; 
            //138     dialogue_processed.responses = responses; 
            //139 

            //140     // 
            //141     // Move the conversation on 
            //142     // 

            Console.WriteLine("{0}. {1}", dialogue.id, dialogue.text);

            if (dialogue.responses != null)
            {
                foreach (Int32 rId in dialogue.responses)
                {
                    Dialogue answer = Dialogues.Find(p => p.id.Equals(rId.ToString()));

                    Console.WriteLine("  >> {0}. {1}", answer.id, answer.text);
                }
            }

            if (dialogue.next != -1)
            {
                UpdateState(actor, player, dialogue.next);
            }
            //146 

            return dialogue;
        }

        public void LoadScript(String actor, String url)
        {
            String[] lines = File.ReadAllLines(url);

            //! Missing is the line 'banana: I hate bananas'.

            foreach (String line in lines)
            {
                Dialogue dialogue = new Dialogue();

                dialogue.actor = actor;
                dialogue.next = -1;

                if (!"1234567890".Contains(line.First()))
                {
                    dialogue.id = line.Substring(0, line.IndexOf(':'));
                    dialogue.text = line.Substring(line.IndexOf(':') + 1).Trim();
                }
                else
                {
                    Int32 start = line.IndexOf(' ') + 1;

                    dialogue.id = line.Substring(0, start - 1);
                    dialogue.actor = actor;

                    if (line.IndexOf("->") != -1)
                    {
                        dialogue.text = line.Substring(start, line.IndexOf("->") - start - 1);
                        dialogue.next = Int32.Parse(line.Substring(line.IndexOf("->") + "->".Length + 1));
                    }
                    else if (line.IndexOf("[") != -1 && line.IndexOf("]") != -1)
                    {
                        dialogue.text = line.Substring(start, line.IndexOf("[") - start - 1);

                        String responses = line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1);

                        dialogue.responses = new List<Int32>();
                        foreach (String response in responses.Split(','))
                        {
                            dialogue.responses.Add(Int32.Parse(response.Trim()));
                        }
                    }
                }

                Dialogues.Add(dialogue);
            }
        }
    }
}
