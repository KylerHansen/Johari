using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {            

            context.Database.EnsureCreated();

            if (context.Adjective.Any()) //look for any categories.
            {
                return; //DB has been seeded
            }

            var adjective = new List<Adjective>
            {
            new Adjective{Name="able",Definition="having the power, skill, means, or opportunity to do something",Type=1},
            new Adjective{Name="accepting",Definition="tending to regard different types of people and ways of life with tolerance and acceptance",Type=1},
            new Adjective{Name="adaptable",Definition="capable of being or becoming adapted (suited by nature, character, or design to a particular use, purpose, or situation)",Type=1},
            new Adjective{Name="bold",Definition="showing or requiring a fearless daring spirit",Type=1},
            new Adjective{Name="brave",Definition="having or showing mental or moral strength to face danger, fear, or difficulty",Type=1},
            new Adjective{Name="calm",Definition="a quiet and peaceful state or condition",Type=1},
            new Adjective{Name="caring",Definition="feeling or showing concern for or kindness to others",Type=1},
            new Adjective{Name="cheerful",Definition="full of good spirits",Type=1},
            new Adjective{Name="clever",Definition="showing intelligent thinking",Type=1},
            new Adjective{Name="complex",Definition="hard to separate, analyze, or solve",Type=1},
            new Adjective{Name="confident",Definition="having a feeling or belief that you can do something well or succeed at something",Type=1},
            new Adjective{Name="dependable",Definition="capable of being trusted or depended on",Type=1},
            new Adjective{Name="dignified",Definition="showing or expressing dignity (the quality or state of being worthy, honored, or esteemed)",Type=1},
            new Adjective{Name="energetic",Definition="operating with or marked by vigor or effect",Type=1},
            new Adjective{Name="extroverted",Definition="possessing or arising from an outgoing and gregarious nature",Type=1},
            new Adjective{Name="friendly",Definition="showing kindly interest and goodwill",Type=1},
            new Adjective{Name="giving",Definition="affectionate and generous where one's feelings are concerned",Type=1},
            new Adjective{Name="happy",Definition="showing or causing feelings of pleasure and enjoyment",Type=1},
            new Adjective{Name="helpful",Definition="of service or assistance",Type=1},
            new Adjective{Name="idealistic",Definition="having a strong belief in perfect standards and trying to achieve them, even when this is not realistic",Type=1},
            new Adjective{Name="independent",Definition="confident and free to do things without needing help from other people",Type=1},
            new Adjective{Name="ingenious",Definition="having a lot of clever new ideas and good at inventing things",Type=1},
            new Adjective{Name="intelligent",Definition="good at learning, understanding and thinking in a logical way about things; showing this ability",Type=1},
            new Adjective{Name="introverted",Definition="more interested in your own thoughts and feelings than in spending time with other people",Type=1},
            new Adjective{Name="kind",Definition="caring about others; gentle, friendly and generous",Type=1},
            new Adjective{Name="knowledgeable",Definition="knowing a lot",Type=1},
            new Adjective{Name="logical",Definition="following or able to follow the rules of logic in which ideas or facts are based on other true ideas or facts",Type=1},
            new Adjective{Name="loving",Definition="feeling or showing love and care for somebody/something",Type=1},
            new Adjective{Name="mature",Definition="behaving in a sensible way",Type=1},
            new Adjective{Name="modest",Definition="not talking much about your own abilities or possessions",Type=1},
            new Adjective{Name="nervous",Definition="easily worried or frightened",Type=1},
            new Adjective{Name="observant",Definition="good at noticing things around you",Type=1},
            new Adjective{Name="organized",Definition="able to plan your work, life, etc. well and in an efficient way",Type=1},
            new Adjective{Name="patient",Definition="able to wait for a long time or accept annoying behaviour or difficulties without becoming angry",Type=1},
            new Adjective{Name="powerful",Definition="being able to control and influence people and events",Type=1},
            new Adjective{Name="proud",Definition="feeling pleased and satisfied about something that you own or have done, or are connected with",Type=1},
            new Adjective{Name="quiet",Definition="(of a feeling or an attitude) definite but not expressed in an obvious way",Type=1},
            new Adjective{Name="reflective",Definition="thinking deeply about things",Type=1},
            new Adjective{Name="relaxed",Definition="calm and not anxious or worried",Type=1},
            new Adjective{Name="spiritual",Definition="believing strongly in a particular religion and obeying its laws and practices",Type=1},
            new Adjective{Name="responsive",Definition="reacting with interest or enthusiasm",Type=1},
            new Adjective{Name="searching",Definition="trying to find out the truth about something; complete and serious",Type=1},
            new Adjective{Name="self-assertive",Definition="very confident and not afraid to express your opinions",Type=1},
            new Adjective{Name="self-conscious",Definition="nervous or embarrassed about your appearance or what other people think of you",Type=1},
            new Adjective{Name="sensible",Definition="able to make good judgements based on reason and experience rather than emotion; practical",Type=1},
            new Adjective{Name="sentimental",Definition="connected with your emotions, rather than reason",Type=1},
            new Adjective{Name="shy",Definition="nervous or embarrassed about meeting and speaking to other people",Type=1},
            new Adjective{Name="silly",Definition="showing a lack of thought, understanding, or judgement",Type=1},
            new Adjective{Name="spontaneous",Definition="often doing things without planning to, because you suddenly want to do them",Type=1},
            new Adjective{Name="sympathetic",Definition="kind to somebody who is hurt or sad; showing that you understand and care about their problems",Type=1},
            new Adjective{Name="tense",Definition="nervous or worried, and unable to relax",Type=1},
            new Adjective{Name="trustworthy",Definition="that you can rely on to be good, honest, sincere, etc.",Type=1},
            new Adjective{Name="warm",Definition="showing enthusiasm, friendship or love",Type=1},
            new Adjective{Name="wise",Definition="able to make sensible decisions and give good advice because of the experience and knowledge that you have",Type=1},
            new Adjective{Name="witty",Definition="clever and humorous",Type=1},
            new Adjective{Name="conscientious",Definition="taking care to do things carefully and correctly",Type=1},
            new Adjective{Name="attentive",Definition="listening or watching carefully and with interest",Type=1},
            new Adjective{Name="incompetent",Definition="not having the skill or ability to do your job or a task as it should be done",Type=0},
            new Adjective{Name="violent",Definition="showing or caused by very strong emotion",Type=0},
            new Adjective{Name="insecure",Definition="not confident about yourself or your relationships with other people",Type=0},
            new Adjective{Name="hostile",Definition="aggressive or unfriendly and ready to argue or fight",Type=0},
            new Adjective{Name="needy",Definition="not confident, and needing a lot of love and emotional support from other people",Type=0},
            new Adjective{Name="ignorant",Definition="not having or showing much knowledge or information about things; not educated",Type=0},
            new Adjective{Name="blase",Definition="not impressed, excited or worried about something, because you have seen or experienced it many times before",Type=0},
            new Adjective{Name="embarrassed",Definition="shy, uncomfortable or ashamed, especially in a social situation",Type=0},
            new Adjective{Name="insensitive",Definition="not realizing or caring how other people feel, and therefore likely to hurt or offend them",Type=0},
            new Adjective{Name="dispassionate",Definition="not influenced by emotion",Type=0},
            new Adjective{Name="inattentive",Definition="not paying attention to something/somebody",Type=0},
            new Adjective{Name="intolerant",Definition="not willing to accept ideas or ways of behaving that are different from your own",Type=0},
            new Adjective{Name="aloof",Definition="not friendly or interested in other people",Type=0},
            new Adjective{Name="irresponsible",Definition="not thinking enough about the effects of what they do; not showing a feeling of responsibility",Type=0},
            new Adjective{Name="selfish",Definition="caring only about yourself rather than about other people",Type=0},
            new Adjective{Name="unimaginative",Definition="not having any original or new ideas",Type=0},
            new Adjective{Name="irrational",Definition="not based on, or not using, clear logical thought",Type=0},
            new Adjective{Name="imperceptive",Definition="lacking in perception or insight",Type=0},
            new Adjective{Name="loud",Definition="talking very loudly, too much and in a way that is annoying",Type=0},
            new Adjective{Name="self-satisfied",Definition="too pleased with yourself or your own achievements",Type=0},
            new Adjective{Name="overdramatic",Definition="excessively dramatic or exaggerated",Type=0},
            new Adjective{Name="unreliable",Definition="that cannot be trusted or depended on",Type=0},
            new Adjective{Name="inflexible",Definition="unwilling to change their opinions, decisions, etc., or the way they do things",Type=0},
            new Adjective{Name="glum",Definition="sad, quiet and unhappy",Type=0},
            new Adjective{Name="vulgar",Definition="not having or showing good taste; not polite, pleasant or well behaved",Type=0},
            new Adjective{Name="unhappy",Definition="not pleased or satisfied with somebody/something",Type=0},
            new Adjective{Name="inane",Definition="stupid or silly; with no meaning",Type=0},
            new Adjective{Name="distant",Definition="not friendly; not wanting a close relationship with somebody",Type=0},
            new Adjective{Name="chaotic",Definition="without any order; in a completely confused state",Type=0},
            new Adjective{Name="vacuous",Definition="showing no sign of intelligence or sensitive feelings",Type=0},
            new Adjective{Name="passive",Definition="accepting what happens or what people do without trying to change anything or oppose them",Type=0},
            new Adjective{Name="dull",Definition="not interesting or exciting",Type=0},
            new Adjective{Name="cold",Definition="without emotion; not friendly",Type=0},
            new Adjective{Name="timid",Definition="shy and nervous; not brave",Type=0},
            new Adjective{Name="stupid",Definition="showing a lack of thought or good judgement",Type=0},
            new Adjective{Name="lethargic",Definition="without any energy or enthusiasm for doing things",Type=0},
            new Adjective{Name="unhelpful",Definition="not helpful or useful; not willing to help somebody",Type=0},
            new Adjective{Name="brash",Definition="confident in an aggressive way",Type=0},
            new Adjective{Name="childish",Definition="behaving in a stupid or silly way",Type=0},
            new Adjective{Name="impatient",Definition="annoyed by somebody/something, especially because you have to wait for a long time",Type=0},
            new Adjective{Name="panicky",Definition="very anxious about something; feeling or showing panic",Type=0},
            new Adjective{Name="smug",Definition="looking or feeling too pleased about something you have done or achieved",Type=0},
            new Adjective{Name="predictable",Definition="behaving or happening in a way that you would expect and therefore boring",Type=0},
            new Adjective{Name="foolish",Definition="not showing good sense or judgement",Type=0},
            new Adjective{Name="cowardly",Definition="not brave; not having the courage to do things that other people do not think are especially difficult",Type=0},
            new Adjective{Name="simple",Definition="ordinary; not special",Type=0},
            new Adjective{Name="withdrawn",Definition="not wanting to talk to other people; extremely quiet and shy",Type=0},
            new Adjective{Name="cynical",Definition="believing that people only do things to help themselves rather than for good or honest reasons",Type=0},
            new Adjective{Name="cruel",Definition="having a desire to cause physical or mental pain and make somebody suffer",Type=0},
            new Adjective{Name="boastful",Definition="talking about yourself in a very proud way",Type=0},
            new Adjective{Name="weak",Definition="easy to influence; not having much power",Type=0},
            new Adjective{Name="unethical",Definition="not morally acceptable",Type=0},
            new Adjective{Name="rash",Definition="doing something that may not be sensible without first thinking about the possible results; done in this way",Type=0},
            new Adjective{Name="callous",Definition="not caring about other peopleâ€™s feelings, pain or problems",Type=0},
            new Adjective{Name="humorless",Definition="not having or showing the ability to laugh at things that other people think are funny",Type=0},            
                                               
            };

            foreach (Adjective a in adjective)
            {
                context.Adjective.Add(a);
            }
            context.SaveChanges();
          

            //var foodtype = new List<FoodType>
            //{
            //    new FoodType{Name="Beef"},
            //    new FoodType{Name="Chicken"},
            //    new FoodType{Name="Veggie"},
            //    new FoodType{Name="Sugar Free"},
            //    new FoodType{Name="SeaFood"},
            //    new FoodType{Name="Dairy Free"}
            //};

            //foreach (FoodType f in foodtype)
            //{
            //    context.FoodType.Add(f);
            //}

            //context.SaveChanges();

        }
    }
}
