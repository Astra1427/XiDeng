using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XiDeng.Common;
using XiDeng.DataTest;

namespace XiDeng.Data
{
    
    class DataCommon
    {
        public static List<ExerciseLog> ExerciseLogs { get; set; }

        public static ObservableCollection<Skill> SkillsTest = new ObservableCollection<Skill>();
        public static void InitDatas()
        {
            SkillsTest.Add(new Skill()
            {
                ID = 1,
                Name = "俯卧撑",
                Description = "铠甲般的胸肌与钢铁般的肱三头肌",
                Img = Utility.GetImage("series1"),
                Styles = new ObservableCollection<SkillStyle> {
                    new SkillStyle{
                        ID = 1,
                        SkillID = 1,
                        Name = "墙壁式",
                        VideoUri = "ms-appx:///skill_1_style_01.mp4",
                        ActionDescription = "面对墙壁站立，双脚并拢，双臂伸直，与肩同宽，双手平放在墙上，手掌与胸等高。" +
                        "这是该动作的起始姿势。弯曲肘部，直到前额轻触墙面。这是该动作的结束姿势。" +
                        "然后将自己推回到起始姿势，如此重复。",

                        Analysis = "俯卧撑动作共有十式，墙壁俯卧撑只是第一式。既然是第一式，也就最容易的，毫无疑" +
                        "问普通人都能做。墙壁俯卧撑也是第一个有治疗效果的练习。刚受伤、做过手术或身体正处" +
                        "于恢复期的人若想加快恢复速度、尽快拥有强健的体魄，可以选择这个动作。肘、腕、肩（尤" +
                        "其是柔弱的肩袖）极易出现慢性或急性损伤，这项练习能够轻柔地刺激这些部位，并改善血" +
                        "液循环。不熟悉徒手体操的初学者应该以较轻柔的动作开始训练，然后逐步提高自己的运动" +
                        "技巧，循序渐进地增强自己的运动能力。我建议就从这个练习动作开始。",
                        SlowSteady = "只要身体没有残疾、没有严重的伤病或疾病，一般人应该都能完成这个动作。如果你刚" +
                        "好处于伤病或手术的恢复期，那么这个动作就是很好的“测试”，能让你了解自己的身体在" +
                        "恢复期的弱点。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_1_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_1_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 25 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 50},
                    },

                    new SkillStyle{
                        ID = 2,
                        SkillID = 1,
                        Name = "上斜式",
                        VideoUri = "ms-appx:///skill_1_style_02.mp4",
                        ActionDescription = "做这个动作需要借助一个稳固的物体，高度大约是你身高的一半（大概到臀部位置）。" +
                        "桌子、高一些的椅子、工作台、厨房操作台、矮墙、结实的栅栏都是不错的选择。大多数监" +
                        "狱牢房里的洗漱台高度就正好，不过你得保证它足够结实。双脚并拢，身体成一条直线，然" +
                        "后前倾上身，双臂伸直，双手抓住所选物体，与肩同宽。这是该动作的起始姿势。" +
                        "弯曲肘部，放低身体，直到胸部轻触物体顶部。如果你选择的物体高度合适，那么此时你的" +
                        "身体与地面的夹角约为 45°。暂停一会，然后将自己推回到起始姿势，如此重复。",
                        Analysis = "这个动作的难度比第一式（墙壁俯卧撑）高，因为你将自己推回到起始姿势时，身体与" +
                        "地面的夹角更小，这意味着你的上肢肌肉要承受更大的重量。上斜俯卧撑比标准俯卧撑（第" +
                        "五式）容易，对大多数人来说，这个动作对肌肉的要求并不太高，而且它能帮助初学者平稳" +
                        "进步，对康复期的健身者也非常有帮助。",
                        SlowSteady = "到达动作的最低点时，你的身体与地面的夹角约为 45°。初学者如果达不到这么高的" +
                        "水平，那就降低难度（加大倾斜角度，也就是让身体更接近直立）——只要选择高度高于你" +
                        "身体中间点的物体即可。然后，再逐渐减小角度，直到可以轻而易举地完成倾斜 45°的上" +
                        "斜俯卧撑。如果你还想尝试更小的角度，可以利用台阶做此动作——随着能力的提高，你可" +
                        "以逐渐降低支撑物的高度。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_2_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_2_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 20 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 40},
                    },

                    new SkillStyle{
                        ID = 3,
                        SkillID = 1,
                        Name = "膝盖式",
                        VideoUri = "ms-appx:///skill_1_style_03.mp4",
                        ActionDescription = "双脚并拢，双膝着地。双臂伸直，与肩同宽。双手在胸部的正下方，两个手掌平放在地" +
                        "面上。脚踝搭在一起，大腿与上身及头部成一条直线，不要撅屁股或者塌腰。这是该动作的" +
                        "起始姿势。然后以膝盖为支点，弯曲肘部，直到胸部与地面仅一拳之隔。暂" +
                        "停一下，然后将自己推回到起始姿势，如此重复。",
                        Analysis = "膝盖俯卧撑是俯卧撑系列的第三式，是初学者要掌握的重要动作。它是在地面上做的俯" +
                        "卧撑中最容易的一个，起着承前启后的作用—此前的俯卧撑都是站立完成的，后面的俯卧撑" +
                        "全是地面动作，而且难度更高。女士们经常会做膝盖俯卧撑，因为她们的上肢力量相对较弱，" +
                        "不易完成标准俯卧撑，不过这个动作对男士也大有好处。对那些超重或身材走形的人来说，" +
                        "膝盖俯卧撑是不错的起点。因为采用这样的姿势推起上半身相对容易，所以在开始做更难的" +
                        "俯卧撑之前，膝盖俯卧撑是绝佳的热身练习。",
                        SlowSteady = "如果你不能完成标准的膝盖俯卧撑，可以减小动作幅度—不要降低到离地面一拳的距" +
                        "离，而是把动作幅度缩短到你能舒服地完成的程度，同时增加次数（约 20 次）。你要不断练" +
                        "习（保持高反复次数），逐渐增加动作深度，直至可以完成标准的膝盖俯卧撑。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_3_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_3_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 30},
                    },

                    new SkillStyle{
                        ID = 4,
                        SkillID = 1,
                        Name = "半俯式",
                        VideoUri = "ms-appx:///skill_1_style_04.mp4",
                        ActionDescription = "跪在地板上，双手撑地，双腿向后蹬直。双手与肩同宽，并处于上胸部的正下方。双腿" +
                        "双脚并拢，锁紧身体，使上身、髋部和双腿成一条直线。先伸直手臂，然后降低身体到大约" +
                        "一半臂长的高度，或者直到肘部弯成直角。控制下降高度的绝佳方式就是使用篮球或橄榄球" +
                        "——将球放在髋部下方。这是该动作的起始姿势。接下来，弯曲肘部，直到髋部与" +
                        "球轻轻接触。对大多数人来说，这样可以方便且客观地标示这个动作的最低点。暂" +
                        "停一下，然后用力将自己推回到起始姿势。",
                        Analysis = "半俯卧撑非常重要，要熟练掌握。很多人做俯卧撑的方法都不正确—撅屁股或者塌腰，" +
                        "这是因为他们的腰部肌肉和脊椎肌肉不发达。这个动作可以锻炼你的腰部肌肉和脊椎肌肉，" +
                        "从而能够锁定 a 部，使身体成一条直线。",
                        SlowSteady = "如果你做不了半俯卧撑，可以减小动作幅度。如果你选用的是篮球，那就将其放在膝盖" +
                        "下，而不是髋部下方。伸直手臂，然后慢慢降低身体，直到膝盖与球接触，这相当于四分之" +
                        "一俯卧撑。如果你能做 10 次以上四分之一俯卧撑，那就可以把篮球向上移一点儿，就这样" +
                        "逐步移动篮球，直到其位于髋部下方为止。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_4_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_4_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 12 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 25},
                    },

                    new SkillStyle{
                        ID = 5,
                        SkillID = 1,
                        Name = "标准式",
                        VideoUri = "ms-appx:///skill_1_style_05.mp4",
                        ActionDescription = "跪在地板上，双手撑地，双腿向后蹬直。双腿双脚并拢，双手与肩同宽，并处于上胸部" +
                        "的正下方。双臂伸直，臀部与脊椎成一条直线。这是该动作的起始姿势。接着，弯" +
                        "曲肘部，直至胸部与地面仅一拳之隔。监狱里进行俯卧撑比赛时，计数者会握拳，让小拇指" +
                        "一侧紧贴地面，这样只需数参赛者胸部接触自己大拇指的次数即可。若你是单独锻炼，又想" +
                        "控制动作幅度，并想让身体与地面保持正确距离，可以在胸部正下方放一个棒球或网球" +
                        "。你在做动作的过程中，待胸部碰到球后暂停一下，然后回到起始姿势。",
                        Analysis = "这就是“经典”俯卧撑。大多数人在体育课上学到的就是这个动作。说到俯卧撑，大多" +
                        "数人脑海中浮现的也是这个动作。标准俯卧撑是极好的上身练习动作，可以锻炼我们的手臂、" +
                        "胸部和上肢带肌，而且效果明显。然而无论如何，标准俯卧撑的难度并不是最高的，它在十" +
                        "式中只排第五。",
                        SlowSteady = "你可能感到费解，很多看上去很健硕的家伙都不能正确地完成标准俯卧撑。如果你也一" +
                        "样，那还是找个篮球做半俯卧撑吧！如果你已能很好地完成第四式—当球放在髋部下面时，" +
                        "你能重复此动作 25 次，那么每次训练时你可以把球向前移动几厘米，在次数保持不变的情" +
                        "况下继续练习。当你的下-61 能碰到球时，你再尝试做标准俯卧撑。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_5_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_5_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20},
                    },

                    new SkillStyle{
                        ID = 6,
                        SkillID = 1,
                        Name = "窄距式",
                        VideoUri = "ms-appx:///skill_1_style_06.mp4",
                        ActionDescription = "窄距俯卧撑的起始姿势与标准俯卧撑基本相同（见第五式），只不过需要双手相触——" +
                        "无需重叠，也不需要让双手的拇指与食指构成一个“钻石”，只要两个食指指尖相触就可以" +
                        "了。从手臂伸直的起始姿势开始，慢慢放低身体，直到胸部轻触手背。暂" +
                        "停一下，然后将自己推回到起始姿势。",
                        Analysis = "窄距俯卧撑很古老，它在俯卧撑十式中至关重要，但人们通常更喜欢弹震式俯卧撑或下" +
                        "斜俯卧撑这些花哨的动作，而忽略窄距俯卧撑。这简直是悲剧，因为窄距俯卧撑在攻克单臂" +
                        "俯卧撑之旅中必不可少。大多数人做单臂俯卧撑都会感到吃力，他们会发现很难在身体降到" +
                        "最低点之后再把自己推起来。这是因为此时肘部的弯曲程度最大，而肘部的弯曲角度超过直" +
                        "角时胳膊就很难使上力气。做窄距俯卧撑时，由于双手的特殊位置，当你的身体降到最低点" +
                        "时，肘部的弯曲度比做标准俯卧撑时的更大。这个动作可以锻炼三头肌，并且强化你的肘部" +
                        "与腕部的肌腱。因此，能舒服地做窄距俯卧撑的人在终于要挑战单臂俯卧撑的时候，会更从容一些。",
                        SlowSteady = "如果你做不了双手相触的窄距俯卧撑（如上所述），可以继续做标准俯卧撑，在次数不" +
                        "变的前提下，让双手逐渐靠近，每次靠近几厘米。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_6_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_6_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 20},
                    },

                    new SkillStyle{
                        ID = 7,
                        SkillID = 1,
                        Name = "偏重式",
                        VideoUri = "ms-appx:///skill_1_style_07.mp4",
                        ActionDescription = "双脚并拢，双腿、髋部、上身成一条直线。双臂伸直，双手撑地，并处于上胸部的正下" +
                        "方。一只手稳固地支撑身体，另一只手撑在篮球上，这是该动作的起始姿势。找到" +
                        "平衡之后，尽力将身体的重量均匀地分摊在两只手上。这样做虽然不容易，但一定要坚持。" +
                        "接下来，弯曲肘部，慢慢降低身体，直到胸部轻触撑在篮球上的那只手。暂停一" +
                        "下，然后将自己推回到起始姿势。",
                        Analysis = "这是第一个高级俯卧撑动作，它能够帮助健身者适应由双手俯卧撑向单臂俯卧撑的过" +
                        "渡。你也可以选用一个固定的物体（如砖块），而不用篮球，不过篮球是最好的选择。控制" +
                        "篮球的同时可以锻炼你的肩袖，这有助于你完成难度更高的动作。你还可以选用足球，但篮" +
                        "球还是首选，因为篮球表面粗糙更容易抓握。",
                        SlowSteady = "能正确地完成窄距俯卧撑的人都可以信心十足地尝试这个动作。如果刚开始你觉得有些" +
                        "困难，那是因为你的协调性不好，而不是力量不足。你可以用固定的物体，而不用会滚来滚" +
                        "去的篮球。砖块是个不错的选择，等你可以在一块砖上重复此动作 20 次之后，你就可以尝" +
                        "试将两块砖探起来做这个动作。在你可以在三块垒起的砖上重复此动作 20 次之后，你就可" +
                        "以尝试用篮球练习了。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_7_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_7_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10,IsSingle = 1 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,IsSingle = 1},
                    },

                    new SkillStyle{
                        ID = 8,
                        SkillID = 1,
                        Name = "单臂半式",
                        VideoUri = "ms-appx:///skill_1_style_08.mp4",
                        ActionDescription = "摆出半俯卧撑最高点时的姿势，即将篮球放在髋部下方（见第四式）。将一只手撑在胸" +
                        "部下方的地面上，手臂伸直，另一只手背在身后。这是该动作的起始姿势。接着弯" +
                        "曲肘部，直到髋部轻触篮球。这是该动作的最低点。暂停一下，然后将自己推回到" +
                        "起始姿势。如果你的肱三头肌不够发达，那做该动作时上身很容易发生扭曲。坚持住，整个" +
                        "身体保持一条直线，做所有俯卧撑都应如此。",
                        Analysis = "单臂半俯卧撑是俯卧撑系列的第八式。通过这个练习，训练者可以逐步从双侧练习转为" +
                        "单侧练习。该动作可以提高你的平衡能力，而这对做单臂俯卧撑极其重要。因为只靠单臂发" +
                        "力，所以这个动作也会让手部、腕部和肩部关节为之后的动作做好准备。单臂半俯卧撑在这" +
                        "个系列中不可或缺，你必须掌握。不过由于肘关节只是部分弯曲，所以对一次完整的俯卧撑" +
                        "训练来说，只做这样的练习是不够的。你需要再做一些肘部弯曲角度小于 90°的练习来补" +
                        "充，比如在之后加上窄距俯卧撑或偏重俯卧撑。",
                        SlowSteady = "如果做不了单臂半俯卧撑，你可以把篮球放在膝下，做四分之一单臂俯卧撑。练习一段" +
                        "时间之后，就一点点向前移动篮球，加大动作幅度。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_8_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_8_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10,IsSingle = 1 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,IsSingle = 1},
                    },

                    new SkillStyle{
                        ID = 9,
                        SkillID = 1,
                        Name = "杠杆式",
                        VideoUri = "ms-appx:///skill_1_style_09.mp4",
                        ActionDescription = "摆出做俯卧撑的姿势，身体成一条直线，一只手撑在胸部正下方的地面上，另一只手放" +
                        "在身体外侧的篮球上，靠双脚和撑在地上的那只手支撑身体。双臂伸直，放在球上的手要尽" +
                        "量向远处伸。这是该动作的起始姿势（图 17）。要有控制地慢慢放低身体，直到胸部与地面" +
                        "只有一拳之隔。如果你是独自锻炼，可以像做标准俯卧撑那样，借助棒球或网球控制动作幅" +
                        "度。放低身体时手会顺势把篮球推到远离身体的位置（图 18）。身体降至最低点时，暂停一" +
                        "下，然后将自己推回到起始姿势。",
                        Analysis = "标准杠杆俯卧撑的难度与单臂俯卧撑相差无几，这也正是杠杆俯卧撑在俯卧撑十式中排" +
                        "在第九的原因。你会发现，撑在篮球上的那只手臂几乎帮不上什么忙，这就迫使支撑身体的" +
                        "手臂必须使出全力。如果你还没强大到足以在做单臂俯卧撑时把自己撑起，那你可以先练习" +
                        "杠杆俯卧撑。",
                        SlowSteady = "由于杠杆原理，撑在篮球上的那只手臂如果完全伸直的话就很难用力。你可以让这只手" +
                        "臂的肘部稍稍弯曲，从而让篮球离你的身体近一点儿，这样做杠杆俯卧撑会容易一些。不过" +
                        "不要太过，要是你把篮球放在身体正下方，那这个动作就变成了第七式—偏重俯卧撑。随着" +
                        "你越来越强壮，你可以逐渐让球远离身体，直到可以将手臂伸直做标准的杠杆俯卧撑。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_9_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_9_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10,IsSingle = 1 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,IsSingle = 1},
                    },

                    new SkillStyle{
                        ID = 10,
                        SkillID = 1,
                        Name = "单臂式",
                        VideoUri = "ms-appx:///skill_1_style_10.mp4",
                        ActionDescription = "跪在地板上，一只手撑在你前方的地面上。双腿向后蹬直，用脚趾支撑身体。脊柱与髋" +
                        "部成一条直线，支撑身体的手臂在胸部下方伸直——不要在身体侧面或是靠前的位置。稳定" +
                        "之后，把不起支撑作用的那只手背在身后。这是该动作的起始姿势（图 19）。弯曲肘部，有" +
                        "控制地放低身体，直到下巴与地面大约有一拳之隔（图 20）。在动作的最低点暂停一下，然" +
                        "后将自己推回到起始姿势。",
                        Analysis = "姿势正确的单臂俯卧撑是检验胸部与肘部力量的黄金标准，而且能够让人一见难忘。许" +
                        "多健身者都声称自己能做单臂俯卧撑，但你千万不要被他们蒙骗。当你让他们动真格的时候，" +
                        "你就会发现，他们所谓的单臂俯卧撑就是个笑话：双腿朝两边分开，上身丑陋地扭曲——这" +
                        "是为了更容易做动作，然后他们会用摇摇摆摆、虚弱无力的胳膊猛地将自己推起，而且他们" +
                        "只能完成少数几次反复。毫无疑问，真正能做单臂俯卧撑的人可谓是危险的稀有动物，你要" +
                        "对自己有点儿信心，相信自己也能跻身其间。",
                        SlowSteady = "如果你已经攻克了杠杆俯卧撑，那么单臂俯卧撑对你而言就不是特别恐怖了。但是，如" +
                        "果你还不能标准地完成 5 次单臂俯卧撑，那么你还是回到第九式，确保自己可以标准地完成" +
                        "20 次杠杆俯卧撑。如果你能做到这一点，但做单臂俯卧撑还是有问题，那么请你继续练习" +
                        "杠杆俯卧撑，直到你可以完成 30 次反复，然后再挑战单臂俯卧撑。",
                        TraningPart = "胸肌、肱三头肌",
                        Img1 = Utility.GetImage("Skill_1_Style_10_1"),
                        Img2 = Utility.GetImage("Skill_1_Style_10_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 6, Number = 10,IsSingle = 1 },
                        UpgradeStandard = new Standard{ GroupsNumber = 1, Number = 100,IsSingle = 1},
                    },

                }
            });

            SkillsTest.Add(new Skill
            {
                ID = 2,
                Name = "深蹲",
                Description = "升降机般的大腿",
                Img = Utility.GetImage("series2"),
                Styles = new ObservableCollection<SkillStyle> {
                    new SkillStyle{
                        ID = 1,
                        SkillID = 2,
                        Name = "肩倒立式",
                        VideoUri = "ms-appx:///skill_2_style_01.mp4",
                        ActionDescription = "平躺，双膝弯曲，双手下压。双脚蹬离地面，直到举到空中。在将双腿举起的过程中，" +
                        "顺势把双手撑在下背部，注意上臂要紧贴地面。你现在摆出的姿势是肩倒立—靠双肩、上背" +
                        "部以及上臂支撑身体。要记住，始终用这几个部位支撑身体，不要让颈部受到压力。身体要" +
                        "锁定伸直，髋部不要弯曲。这是该动作的起始姿势（图 21）。上半身要尽可能伸直，弯曲髋" +
                        "部与膝关节，直到膝盖轻触前额，这是该动作的结束姿势（图 22）。然后伸直双腿，直到身" +
                        "体回到起始姿势，如此重复。",
                        Analysis = "对任何开始练习深蹲的人来说，肩倒立深蹲都是完美的起点。由于做该动作时身体处于" +
                        "倒立姿势，所以膝盖和下背部无需承受身体的重量，这就使得该动作成为一个理想的恢复性" +
                        "训练动作—可以帮助那些背部和膝盖有伤的健身者，或是刚做完手术正处于恢复期的健身者" +
                        "重新开始腿部训练。从力量角度来说，做肩倒立其实对健身者上身的要求更高。但是这一动" +
                        "作能让紧绷的关节放松，增加关节的活动幅度，从而能让初学者为练就完美的深蹲打下基础。" ,
                        SlowSteady = "第一次尝试该动作时，不是人人都能做到膝盖轻触前额。你可以在每次锻炼时试着加大" +
                        "动作幅度，这样你的关节很快就会得到放松。另外，有啤酒肚的人几乎无法完成这个动作，" +
                        "因为他的大肚子会碍事。对这类人来说，坚持空腹练习会好一些——直到他们减掉肚子上的" +
                        "赘肉。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_1_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_1_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 25 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 50},
                    },
                    new SkillStyle{
                        ID = 2,
                        SkillID = 2,
                        Name = "折刀式",
                        VideoUri = "ms-appx:///skill_2_style_02.mp4",
                        ActionDescription = "站在一个稳固的物体前，此物体的高度大约与你的膝盖等高，至少也要达到胫骨上部，" +
                        "小咖啡桌、椅子、床铺都是不错的选择。双腿分开，与肩同宽或略宽。双腿伸直，弯腰俯身，" +
                        "直到双手与面前的物体接触。身体前倾，使一部分体重落在双手上。这是该动作的起始姿势" +
                        "（图23）。上半身尽量与地面平行，弯曲膝关节和髋部，直到大腿后侧紧贴小腿，无法蹲得" +
                        "更低为止。这是该动作的最低点（图24）。下蹲过程中你还需要弯曲双臂，在下降到最低点" +
                        "后腿部与手臂要同时发力，将身体推回到起始姿势。在整个动作过程中，脚跟始终不能抬离" +
                        "地面。",
                        Analysis = "做折刀深蹲时上半身前倾，并不直接位于双腿上方，因此两条手臂会承担一部分体重。" +
                        "该动作的难度大约只有标准深蹲（第五式）的一半，但这是能帮助下身肌肉与肌腱为之后的" +
                        "动作做准备的最佳动作。只要正确练习，这个动作就能让初级健身者拥有足够的平衡能力和" +
                        "跟腱的柔韧性，来征服标准深蹲中的最低点。" ,
                        SlowSteady = "下降到最低点时该动作的难度最大，因为此时下肢需要承担身体的大部分重量。如果你" +
                        "觉得动作有难度，那就逐步加大动作幅度来慢慢适应。还有一种方法就是让双臂分担更多的" +
                        "身体重量，这样能够帮助你从最低点站起来。等腿部更强壮之后，你就可以试着减少双臂的" +
                        "用力，更多地依赖双腿。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_2_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_2_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 20 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 40},
                    },

                    new SkillStyle{
                        ID = 3,
                        SkillID = 2,
                        Name = "支撑式",
                        VideoUri = "ms-appx:///skill_2_style_03.mp4",
                        ActionDescription = "直立，双脚分开，与肩同宽或略宽。手臂向斜下方伸出，双手放在比自己的大腿略高的" +
                        "稳固物体上，书桌、高脚凳或椅背都可以。这是该动作的起始姿势（图25）。弯曲髋部与膝" +
                        "关节，身体慢慢下降，背部尽可能保持挺直，直到大腿后侧紧贴小腿，无法蹲得更低为止，" +
                        "这是该动作的最低点（图26）。暂停一会，然后主要靠腿部发力站起来。为了分担双腿的一" +
                        "部分压力，尤其是在最低点时，你要通过向下按压面前的物体，来借用手臂的一部分力量。" +
                        "手臂要尽量伸直，在整个动作过程中都不要让脚跟抬离地面。",
                        Analysis = "支撑深蹲是半深蹲之前的最后一式。它是折刀深蹲（双腿承受大部分身体重量）与半深" +
                        "蹲（双腿承受全部身体重量）之间理想的过渡动作。支撑深蹲能够进一步加强下肢的柔韧性" +
                        "和力量，锻炼膝盖的肌腿、韧带及软组织。它能让你的深蹲动作更标准，锻炼你仅仅使用肌" +
                        "肉力量而非惯性从最低点推起身体的能力。" ,
                        SlowSteady = "要想对该练习所需的腿部力量进行微调，方法非常简单。要使该练习对下身来说更容易，" +
                        "只要更多地使用上身的力量即可。随着你在动作最低点时感觉越来越轻松，你可以逐渐减少" +
                        "在起身过程中臂力的使用。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_3_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_3_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 30},
                    },

                    new SkillStyle{
                        ID = 4,
                        SkillID = 2,
                        Name = "半蹲式",
                        VideoUri = "ms-appx:///skill_2_style_04.mp4",
                        ActionDescription = "站立，双脚分开，与肩同宽或略宽。脚尖不要步勃M 前方，而要略微向外。双手放在" +
                        "髋部、胸部、肩部都可以——以舒适为前提。这是该动作的起始姿势（图27）。弯曲髋部和" +
                        "膝盖，直到膝关节弯曲成90°——换句话说就是大腿与地面平行。这是该动作的最低点（图" +
                        "28）。刚开始你可以借助一面镜子或是找朋友帮忙，直到能够自如地控制动作幅度。不要求" +
                        "快，也不要借助惯性起身，而要完全在肌肉控制下做整个动作。在最低点（半空中）坚持1" +
                        "秒钟，然后再回到起始姿势。在整个动作过程中，背部始终要挺直，双脚脚跟始终不能抬离" +
                        "地面。膝盖与脚尖应该始终朝向同一方向，深蹲时膝盖绝不要向内转，脚尖指向外有助于你" +
                        "做到这一点。",
                        Analysis = "在深蹲系列中，半深蹲是双腿在没有任何辅助的情况下承受全部体重的第一式。因此，" +
                        "它应该受到重视。该动作教你在无辅助的情况下保持平衡和身体姿势，这些对于后面的深蹲" +
                        "练习都是必要的。此外，该动作也能够让你了解如何让膝盖与双脚摆出对你来说最适合的姿" +
                        "势。这个动作对大腿而言难度并不大，因此下面给出的训练目标量比正常的量要大。攻克了" +
                        "这个动作之后，你会发现自己髋部和大腿内侧的肌肉变得更强了。" ,
                        SlowSteady = "如果做不了半深蹲，那就先做四分之一深蹲，每当你感到有余力时，就可以适当加大动作幅度。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_4_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_4_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 35 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 50},
                    },

                    new SkillStyle{
                        ID = 5,
                        SkillID = 2,
                        Name = "标准式",
                        VideoUri = "ms-appx:///skill_2_style_05.mp4",
                        ActionDescription = "直立，双脚分开，与肩同宽或略宽（取决于个人偏好）。双脚略微向外转，双臂随意摆" +
                        "放，只要舒服即可。这是该动作的起始姿势（图29）。髋部与膝关节弯曲，背部始终挺直。" +
                        "当大腿达到几乎与地面平行时，把你的身体重心向后转移，就像要坐下一样。有控制地继续" +
                        "放低身体，直到大腿后侧紧贴小腿。这是该动作的最低点（图30）。暂停一会，然后仅靠腿" +
                        "部发力将自己推回到起始姿势。起身过程和下蹲过程应该是完全相反的。脚跟始终不要抬离" +
                        "地面，膝盖也不能向内转。",
                        Analysis = "标准深蹲是经典的自身体重练习动作。数千年来该动作始终流行于世，是有一定原因的。" +
                        "标准深蹲能够强化膝关节，并增强大腿肌肉、臀部肌肉、脊椎肌肉和髋部肌肉的力量和运动" +
                        "能力。另外，这个动作也能使小腿、胫骨前肌、脚踝，甚至包括脚掌得到相应的锻炼。标准深蹲有助于让双腿保持年轻活力。" ,
                        SlowSteady = "如果你已经达到了半深蹲的升级标准，那么做标准深蹲就不会有太大问题。由于杠杆原" +
                        "理，当动作到达最低点时难度最大，对个子较高的人来说尤其如此。如果你达不到初级标准" +
                        "的目标，那就继续去做半深蹲。如果你感到自己变强了，那就增加几厘米的动作幅度。不要" +
                        "心急，一定不要借助惯性起身、让脚跟离地或是摇晃前倾。要纯粹使用肌肉力量，否则就别" +
                        "练了！",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_5_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_5_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 30},
                    },

                    new SkillStyle{
                        ID = 6,
                        SkillID = 2,
                        Name = "窄距式",
                        VideoUri = "ms-appx:///skill_2_style_06.mp4",
                        ActionDescription = "直立，双脚脚跟相碰，脚尖微微向外，双臂前伸。这是该动作的起始姿势（图31）。弯" +
                        "曲膝盖和舰部，直到大腿后侧紧贴小腿，无法蹲得更低为止。此时，你的胸部应该紧贴大腿" +
                        "（图32）。注意，脚跟始终不要抬离地面。为避免后倾，你得收缩胫骨肌肉使身体微微前倾。" +
                        "保持这一姿势，然后仅靠腿部发力将自己推回到起始姿势。",
                        Analysis = "窄距深蹲具有标准深蹲的所有益处，但是对股四头肌的影响更大。经常练习该动作真的" +
                        "能强化你的膝盖、胫骨和臀部肌肉，让你的臀部更紧实，效果比任何一种器械训练都好。" ,
                        SlowSteady = "很多训练者练习前几式时进展神速，但到了窄距深蹲就会碰到问题——在动作最低点或" +
                        "接近最低点时，身体有可能失去平衡而后倾。这些问题在腿骨长、个头高的训练者身上尤为" +
                        "突出。之所以会出现这些问题，是因为训练者胫骨前侧的肌肉缺乏力量以及平衡能力不强。" +
                        "如果练习前几式时你的进度太快，那就回到标准深蹲，在练习过程中逐渐缩小双脚之间的距" +
                        "离——每次缩小几厘米。双臂前伸能够让重心前移。此外，你也可以手拿重物——如哑铃、" +
                        "书或矿泉水瓶等——练习该动作，不过最好还是空手能完成。有些训练者由于身体结构的原" +
                        "因，做该动作真的很困难，如果你就是这样，那么可以让双脚脚跟保持一掌的距离。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_6_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_6_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20},
                    },

                    new SkillStyle{
                        ID = 7,
                        SkillID = 2,
                        Name = "偏重式",
                        VideoUri = "ms-appx:///skill_2_style_07.mp4",
                        ActionDescription = "直立，一只脚踩在地上，另一只脚的脚跟放在位于自己前方、距身体约一步远的篮球上。" +
                        "双脚分开，与肩同宽或略宽，双臂在胸前伸直。这是该动作的起始姿势（图33）。弯曲膝盖" +
                        "和髋部，直到踩在地上的那条腿的大腿后侧紧贴小腿后侧。这是该动作的最低点（图34）。" +
                        "刚开始练习时，到达最低点后可能出现身体后倾的情况，所以要确保身后有足够的干净空间，" +
                        "这一点对所有全幅深蹲都适用。暂停一下，然后双腿发力把自己推回到起始姿势。在动作过" +
                        "程中，脚跟始终不要抬离地面，身体不要向前摇晃，尽管你可能不自觉地想这样做。切记，" +
                        "要有控制地完成整个动作。",
                        Analysis = "偏重深蹲是到挑战单腿深蹲的第一步。到现在为止，深蹲系列中各式的动作都是对称的" +
                        "——都能够均衡地锻炼双腿。练习该动作时，放在篮球上的那条腿位置相对较高，而且又必" +
                        "须控制篮球，所以无法完全发力。踩在地上的那条腿需要承担大部分工作，要有足够的力量" +
                        "在另一条腿的辅助下把身体从最低点推起。这个动作还可以很好地改善你的平衡能力与协调" +
                        "能力。" ,
                        SlowSteady = "与前几式相比，该动作需要更多的技巧和更大的力量。如果把脚架在篮球上难以保持平" +
                        "衡，那你可以选用稳固的物体（如三块垒起的砖头）代替篮球。如果这样还是有问题，你可" +
                        "以选择降低物体的高度（如一块砖）。随着你的信心与平衡能力的增强，可以逐渐增加物体" +
                        "的高度。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_7_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_7_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10,IsSingle = 1 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,IsSingle = 1},
                    },

                    new SkillStyle{
                        ID = 8,
                        SkillID = 2,
                        Name = "单腿半蹲式",
                        VideoUri = "ms-appx:///skill_2_style_08.mp4",
                        ActionDescription = "直立，只靠一条腿站立，另一条腿向前抬起，伸直或略微弯曲，抬起的脚大约处在另一" +
                        "条腿大腿的高度上，双手向胸前伸出。这是该动作的起始姿势（图35）。弯曲髋部和支撑腿" +
                        "的膝盖，直到膝关节几乎弯曲成90°，即大腿几乎与地面平行。在此过程中，抬起的那条" +
                        "腿应该始终在空中。这是该动作的最低点（图36）。暂停一会，然后单腿发力把自己推回到" +
                        "起始姿势。在动作过程中，背部始终要保持平直，而且支撑腿的脚跟始终不能抬离地面。",
                        Analysis = "该练习是深蹲系列中第一个完全的单腿动作。攻克这个动作很重要，因为这可以让健身" +
                        "者的平衡能力得到极大的提升，从而为标准单腿深蹲打下基础。通过这个动作，健身者能够" +
                        "逐步掌握非支撑腿长时间离地的技巧。这可不容易，因为这需要非常强壮的a 部屈肌，可大" +
                        "多数人的髋部屈肌都很弱。由于只有一条腿支撑身体，所以这个动作可以更好地增强我们的" +
                        "腿部力量。不过由于该动作的运动幅度只有正常的一半，所以在练习该动作时，应该辅以一" +
                        "个全幅动作—窄距深蹲或偏重深蹲都可以。" ,
                        SlowSteady = "达到偏重深蹲升级标准的健身者，做该动作应该不成问题。如果你发现这个动作对你来" +
                        "说还是个挑战的话，那就先使用更小的动作幅度，随着体能的发展再逐步增加下蹲的深度即" +
                        "可。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_8_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_8_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,IsSingle = 1},
                    },

                    new SkillStyle{
                        ID = 9,
                        SkillID = 2,
                        Name = "单腿辅助式",
                        VideoUri = "ms-appx:///skill_2_style_09.mp4",
                        ActionDescription = "把篮球放在要练的那条腿的外侧。直立，一只脚平放在地面上，另一只脚在你前方抬起" +
                        "——同单腿半深蹲（第八式）的起始姿势。抬起那条腿的同侧手臂向前伸出，另一只手臂自" +
                        "然垂于体侧（图37）。然后弯曲髋部和支撑腿的膝盖，直到大腿后侧紧贴小腿，无法继续下" +
                        "蹲为止。此时，你的手应该稳稳地放在篮球上，这是该动作的最低点（图38）。起身时主要" +
                        "依靠腿部力量，但你也要用手按压篮球，以便在反向动作的最初阶段借力。注意，在动作过" +
                        "程中，支撑腿的脚跟始终不能抬离地面。",
                        Analysis = "任何一种深蹲动作的最低点都是最难的，单腿深蹲尤其如此。该动作通过允许你做反向" +
                        "动作时在至关重要的最初几厘米中用手辅助、借力，安全地解决了在最低位置时的问题。通" +
                        "过练习该动作，你膝关节处的韧带与肌腱将变强，之后你就可以信心十足地开始挑战最终式" +
                        "——单腿深蹲。另外，该动作还会迫使髋部屈肌更加用力，以保持抬起的那条腿比做单腿半" +
                        "深蹲时抬得更高，而这可能需要你花点儿时间才能适应。在这重要的一式上投入点时间。" ,
                        SlowSteady = "如果你还达不到该练习的初级标准，那就试着使用比篮球高的物体，椅子或矮咖啡桌都" +
                        "是不错的选择。相比于使用篮球，这些物体可以在更大的运动范围内为你的手臂提供支撑。" +
                        "一旦你借助更高物体能完成该动作，就可以换用矮一些的物体，就这样循序渐进地练习，直" +
                        "到你准备好再次尝试用篮球做该动作。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_9_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_9_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,IsSingle = 1},
                    },

                    new SkillStyle{
                        ID = 10,
                        SkillID = 2,
                        Name = "单腿式",
                        VideoUri = "ms-appx:///skill_2_style_10.mp4",
                        ActionDescription = "直立，抬起一条腿，直到脚大约与髋部等高，腿要尽量伸直，双臂在胸前前伸。如果你" +
                        "在之前几式上已经花了大量时间，那么这些对你来说不会太难。这是该动作的起始姿势（图" +
                        "39）。然后弯曲髋部与支撑腿的膝盖。你应该有控制地放低身体，不要一下蹲到底。缓慢地" +
                        "下蹲，直到支撑腿的大腿后侧紧贴小腿，无法继续下蹲为止。此时，你的腹部应该与支撑腿" +
                        "的大腿紧贴在一起。这是该动作的最低点（图40）。在紧张状态下，暂停一下（数一个数），" +
                        "然后单腿发力把自己推回到起始姿势。注意，千万不要借助惯性起身。在动作过程中，背部" +
                        "始终挺直，抬起的腿始终离地，而支撑腿则应稳稳地踩在地上。到达动作最高点时暂停一下，" +
                        "然后再下蹲。",
                        Analysis = "单腿深蹲是深蹲动作之王，也是锻炼下身力量的终极练习。它可以锻炼脊椎、髋部、大" +
                        "腿、小腿和双脚，还能增强我们的耐力和运动能力。久而久之，皮包骨的双腿将变成强有力" +
                        "的支柱——有钢索般的股四头肌、硬如岩石的臀大肌、壮硕有形的小腿。掌握这个动作，你" +
                        "的双腿就绝不会衰老。而且，它还会保护你免受髋部病痛与膝关节损伤的折磨。" ,
                        SlowSteady = "如果你达不到单腿深蹲的初级标准，那就回到第九式（单腿辅助深蹲），并使用比篮球" +
                        "略小的物体，如三块裸起的砖。逐渐使用越来越小的物体，直到根本无需支撑。",
                        TraningPart = "臀部、大腿",
                        Img1 = Utility.GetImage("Skill_2_Style_10_1"),
                        Img2 = Utility.GetImage("Skill_2_Style_10_2"),
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 50,IsSingle = 1},
                    },

                },

            });

            SkillsTest.Add(new Skill
            {
                ID = 3,
                Name = "引体向上",
                Description = "仓门般的背部与大炮般的肱二头肌",
                Img = Utility.GetImage("series3"),
                Styles = new ObservableCollection<SkillStyle> {
                    new SkillStyle{
                        ID = 1,
                        SkillID = 3,
                        Name = "垂直式",
                        Img1 = Utility.GetImage("Skill_3_Style_1_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_1_2"),
                        VideoUri = "ms-appx:///skill_3_style_01.mp4",
                        ActionDescription = "找一个可抓握且很稳固的竖直物体，门框和高一点的栏杆都是上好之选。靠近物体站立，" +
                        "脚尖与之保持约 8 一巧厘米的距离。以舒服的姿势抓住该物体，理想情况是双手与肩同宽，" +
                        "但不必要——只要双手对称即可。这是该动作的起始姿势（图 41）。由于你距离物体很近，" +
                        "所以手臂会弯曲。身体慢慢向后倾，在此过程中伸展手臂，直到手臂几乎伸直、身体后倾与" +
                        "地面成一定角度为止。这是该动作的结束姿势（图 42）。此时，你的上背部应该有拉伸感，" +
                        "手臂可能也会有同感。暂停一会，再并拢肩脚骨并弯曲手臂，把身体拉回到起始姿势。暂停，" +
                        "然后再重复该动作。",
                        Analysis = "垂直引体是动作非常轻微的引体向上动作。对那些背部和手臂力量正处于恢复阶段的训" +
                        "练者来说，这是非常理想的练习。此外，这个动作对肩部、肱二头肌或肘部受过伤的训练者" +
                        "而言更适合，因为它可以增加血液流动，并让他们的身体找回“拉力”的感觉。对初学者来" +
                        "说，这也是上好的练习。由于该动作强度较小，可以使初尝拉力训练的人，在进人难度更大" +
                        "的动作之前真切地感受肩部和上背部肌肉的“发力”。",
                        SlowSteady = "垂直引体几乎是人人都能做的简易练习。如果你正处在伤病康复阶段，觉得该动作对某" +
                        "些身体部位（也许是缝针部位）而言太过剧烈，那就减小动作幅度，绷紧肩部，别让手臂伸" +
                        "得太直。",
                        TraningPart = "背部、肱二头肌",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 25 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 40},
                    }, 
                    new SkillStyle{
                        ID = 2,
                        SkillID = 3,
                        Name = "水平式",
                        VideoUri = "ms-appx:///skill_3_style_02.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_2_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_2_2"),
                        ActionDescription = "找一个至少与你的髋部等高、稳固且双手可抓握的水平物体。该物体要能安全地承载你" +
                        "的体重，又大又结实的桌子（如餐桌和书桌）通常是最佳之选。钻到桌子下面——胸部与下" +
                        "肢都位于桌子下面，抬手抓住桌子边缘（使用正握姿势）。理想情况是，双手与肩同宽，但" +
                        "这要取决于你用的是什么样的桌子。然后拉起身体，使背部离地，只有脚跟与地面接触。有" +
                        "时你的手臂可能需要适当弯曲才能使背部离地——这取决于桌子的高度。身体绷紧，让双手" +
                        "和双脚脚跟承担身体的重量。这是该动作的起始姿势（图43）。然后平缓地拉起身体，在此" +
                        "过程中整个身体（尤其是膝盖）要成一条直线，直到胸部触到桌子边缘。这是该动作的结束" +
                        "姿势（图44）。暂停一下，然后降低身体，回到起始姿势，如此重复。",
                        Analysis = "水平引体向上与垂直引体类似，但练习水平引体向上时身体会更加倾斜，因此该动作对" +
                        "力量的要求更高。这是非常好的过渡练习，在开始后续的悬垂式练习之前，训练者一定要掌" +
                        "握它。该动作也可以强健某些关节，尤其是易于受伤的肘关节和肩关节。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "所用物体越高，该练习也就更容易。如果刚开始你觉得太难，那就找一些比自己髋部略" +
                        "高的物体。等你能做30 次反复之后，再尝试用与髋部等高的物体来练习。在该动作中，双" +
                        "脚会在地面上适当滑动，如果这个过程中的摩擦对你构成了明显阻力，那么可以试着使用更" +
                        "容易滑动的鞋或地板，或者双手抓在吊环类的物体上——这样双手的位置就可以在动作过程" +
                        "中适时调整，从而免除了双脚滑动的必要性。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 20 ,},
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 30,},
                    },
                    new SkillStyle{
                        ID = 3,
                        SkillID = 3,
                        Name = "折刀式",
                        Img1 = Utility.GetImage("Skill_3_Style_3_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_3_2"),
                        VideoUri = "ms-appx:///skill_3_style_03.mp4",
                        ActionDescription = "练习折刀引体向上需要高一点的横杆，横杆下面放一把高脚凳或类似物体。向上跳起抓" +
                        "住横杆，手臂大约与肩同宽，采取正握姿势。用横杆训练时，肩部始终要收紧（参见第107" +
                        "页），手臂也不能完全放松，肘部微微弯曲。将双腿向上摆，把双脚脚跟搭在横杆前方的高" +
                        "脚凳上，双腿要完全伸直。该物体要足够高，理想情况是双腿伸直时脚踩与骨盆恰好在同一" +
                        "高度，此即经典的折刀角度。这是该动作的起始姿势（图45）。然后平缓地把身体拉起，伸" +
                        "直的双腿向下压以帮助完成动作，最后使下巴高过横杆。这是该动作的结束姿势（图46）。" +
                        "暂停一下，然后降低身体，在肌肉的完全控制下回到起始姿势。注意，每组练习都不要做到" +
                        "“精疲力竭”，好在练习结束后能安全地下来。如果你还没站稳就松开双手，可能会跌倒受" +
                        "伤。",
                        Analysis = "折刀引体向上是全幅度引体向上动作。由于双腿晏承担一部分体重，在动作最低点时还" +
                        "可以起协助作用，所以该练习比标准引体向上容易。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "最低点是所有引体向上动作中最难的部分。如果你做不了全幅的折刀引体向上，那就先" +
                        "集中练习上半部分的动作幅度，也就是手臂保持一定的弯曲度，随着力量的增长再逐渐增加" +
                        "动作幅度。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 ,},
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 20,},
                    },
                    new SkillStyle{
                        ID = 4,
                        SkillID = 3,
                        Name = "半引体式",
                        VideoUri = "ms-appx:///skill_3_style_04.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_4_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_4_2"),
                        ActionDescription = "选择足够高的横杆，使得身体悬垂在上面时双脚依然离地，即便只离地一厘米也可以。" +
                        "向上跳起抓住横杆，采用正握姿势，两手与肩同宽或略宽，双臂弯曲接近90°（上臂应与" +
                        "地面平行），肩部始终收紧。膝部微屈，脚躁交叠在一起，以免双腿辅助借力。这是该动作" +
                        "的起始姿势（图47）。弯曲肘部，夹起肩部，平缓地拉起身体，直到下巴超过横杆。这是该" +
                        "动作的结束姿势（图48）。在最高处暂停一下，然后有控制地下降到起始姿势。在动作过程" +
                        "中肘部可以向前移动，但两腿应始终保持不动。",
                        Analysis = "现在要来真格的了！在做半引体向上时，上身肌肉要支撑你全身的重量，这肯定比你舒" +
                        "坦地划船或做下拉动作强度大。因此，你的抓握能力会大大增强，背部、肱二头肌和前臂也" +
                        "能够得到锻炼。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "在引体向上系列中，该动作是训练者在无辅助条件下移动身体的第一个动作。对许多训" +
                        "练者来说（尤其是那些体重略重或超重的家伙），此动作是一个“暂停点”。如果你的身上有" +
                        "赘肉（大多数人都有），那在这一点上就需要减重。在减重过程中你依然可以坚持练习该动" +
                        "作。如果觉得有困难，就减少动作幅度，下巴只需接近横杆即可。随着体重减轻，你可以逐" +
                        "步增加动作幅度。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 11 ,},
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 15,},
                    },
                    new SkillStyle{
                        ID = 5,
                        SkillID = 3,
                        Name = "标准式",
                        VideoUri = "ms-appx:///skill_3_style_05.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_5_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_5_2"),
                        ActionDescription = "以正握姿势握住横杆，双手与肩同宽或略宽——你可以试试看多大宽度对你来说最容易" +
                        "发力。双脚离地，双膝微屈，脚踝交叠在一起并置于身后。身体绷紧，双肩收紧，肘部略微" +
                        "弯曲（几乎看不出来），让肌肉而不是肘关节承担压力。这是该动作的起始姿势（图49）。" +
                        "弯曲肘部，夹起肩部，直至下巴超过横杆。这是该动作的结束姿势（图50）。欣赏一下上面" +
                        "的风景吧！暂停一会，然后有控制地反向运动。不要做爆发式动作，否则惯性就会参与进来。" +
                        "平缓的动作是练出肌肉的完美技巧。试着用2 秒钟将自己拉起来，再用2 秒钟缓慢地放低身" +
                        "体，并在动作的最高点和最低点各停顿1 秒钟。",
                        Analysis = "标准引体向上是锻炼上背部与肱二头肌的经典练习。掌握这个动作后，你将具有出众的" +
                        "行动能力与运动力量。人类天生就善于把自己拉起来，一个男人要是连引体向上都做不了，" +
                        "那就称不上真的强壮。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "标准引体向上是一个强度颇大的体操动作，如果你觉得该动作比较难，那么很多人都和" +
                        "你有同样的感受。该动作的关键在于坚持不懈，你一定要忍住，不要早早“蹬腿” （参见" +
                        "第108 页），因为那会让你养成坏习惯。如果你需要借力才能走出困境（即手臂完全伸直时），" +
                        "那就把一只脚放在椅子上，轻轻向下压。每次训练时都减少一点这只脚的用力，直到最终只" +
                        "在刚开始的三四厘米中才用脚借力。最后，你就能在没有协助的情况下做标准引体向上了。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 8 ,},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 10,},
                    },
                    new SkillStyle{
                        ID = 6,
                        SkillID = 3,
                        Name = "窄距式",
                        VideoUri = "ms-appx:///skill_3_style_06.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_6_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_6_2"),
                        ActionDescription = "向上跳起抓住横杆，采用正握姿势，双手尽量挨在一起—如果距离太近关节会感到不适，" +
                        "但最宽也不要超过10 厘米。双膝弯曲，双脚脚跺交叠在一起并置于身后，以免腿部借力。" +
                        "肘部微微弯曲，双肩收紧。这是该动作的起始姿势（图51）。弯曲肘部，夹起肩部，平缓地" +
                        "将自己拉起来，直到下巴高过横杆。这是该动作的结束姿势（图52）。暂停一下，然后再慢" +
                        "慢放低身体，回到起始姿势。暂停，然后再重复。整个运动过程中，腿部尽量保持不动。",
                        Analysis = "在所有的引体向上动作中，短板都是手臂上的屈肌，即肱二头肌与相关的前臂肌肉。如" +
                        "果训练者已经掌握了双臂引体向上，想升级到单臂引体向上，那就得花时间极大地强化肱二" +
                        "头肌—这就是窄距引体向上的用意所在。两手紧挨在一起时，更大、更强的背部肌肉不容易" +
                        "发力，这样就迫使手臂上的屈肌承担更大的负荷，从而让它们（尤其是肱二头肌）变得更大、" +
                        "更有力。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "有些练习标准引体向上的训练者会觉得窄距引体向上有点儿难，因为双手间距缩小的" +
                        "话，我们在上拉身体的时候手臂会不自觉地向内扭转—正握的姿势有时会限制这种自然倾" +
                        "向。这是试验各种抓握姿势的好时候，你不妨尝试一下侧握或反握姿势。如果条件允许，也" +
                        "可以尝试使用吊环（参见第108 页）。如果力量不足，那就继续练习标准引体向上，每次练00" +
                        "习时把两手的距离缩小两厘米，久而久之你便会掌握窄距引体向上。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 8 ,},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 10,},
                    },
                    new SkillStyle{
                        ID = 7,
                        SkillID = 3,
                        Name = "偏重式",
                        VideoUri = "ms-appx:///skill_3_style_07.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_7_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_7_2"),
                        ActionDescription = "单手抓住横杆。采用侧握或反握姿势会比经典的正握姿势舒服。用另一只手抓住握横杆" +
                        "那只手的手腕—大拇指位于那只手的手掌下方，其他手指则位于其手背下方。双脚离地，双" +
                        "膝弯曲，脚跺交叠在一起并置于身后。始终收紧上肢带肌，抓横杆那只手臂伸直，但肘部要" +
                        "略微弯曲。另一只手臂由于位置较低，弯曲程度会比较大。你的双肘会朝向前方。这是该动" +
                        "作的起始姿势（图53）。弯曲肘部，夹起肩部，平缓地将自己拉起来，直到下巴高过横杆。" +
                        "这是该动作的结束姿势（图54）。暂停一下，然后慢慢放低身体，回到起始姿势。再次暂停" +
                        "并重复。",
                        Analysis = "偏重引体向上的历史可以追溯到几百年前，但此动作再度流行则是因为电影《洛奇2》" +
                        "—史泰龙用该动作进行训练。由于手臂的位置不同，抓握横杆的那只手臂不得不承担大部分" +
                        "的工作，这将为做单臂引体向上打下基础。该动作能够增强背阔肌、肱二头肌以及背部肌肉" +
                        "的力量，同时也会极大地锻炼抓握能力。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "如果你能做窄距引体向上，那应该也能做偏重引体向上。这两个动作最大的不同在于，" +
                        "偏重引体向上中你只能用单手吊起身体。如果你觉得吃力，那你在做引体向上之后就要花点" +
                        "儿时间练习单臂悬垂以增强抓握能力。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 7 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 8,IsSingle = 1},
                    },
                    new SkillStyle{
                        ID = 8,
                        SkillID = 3,
                        Name = "单臂半引体式",
                        VideoUri = "ms-appx:///skill_3_style_08.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_8_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_8_2"),
                        ActionDescription = "单手抓住高过头顶的横杆，要选择对你来说最容易发力的抓握姿势——有人是正握，有" +
                        "人是侧握或反握，但对大多数人来说，在吊环上做该动作可能最容易。另一只手可随意放置，" +
                        "舒服即可。我的大多数学生喜欢将手臂悬于空中，我个人更喜欢像做单臂俯卧撑那样将手置" +
                        "于身后。哪种姿势都可以，只要不妨碍你做动作就好。将要锻炼的那只手臂（可以通过跳、" +
                        "蹬腿或借助一把椅子等方式）置于半弯曲状态，肘部弯曲成直角，即上臂与地面平行。双脚" +
                        "离地，脚踩交叠在一起并置于身后。发力的那侧肩部应该收紧，同时全身都要绷紧。这是该" +
                        "动作的起始姿势（图55）。弯曲肘部，夹起肩部，平缓地将自己拉起来，直到下巴高过横杆。" +
                        "这是该动作的结束姿势（图56）。暂停一下，然后慢慢放低身体，回到起始姿势。在起始姿" +
                        "势暂停，再重复练习。",
                        Analysis = "在引体向上系列中，这是第一个用单臂拉起全部体重的动作。该动作可以帮助训练者高" +
                        "平衡能力，熟悉标准单臂引体向上所要求的发力方法。该动作还会发展肱二头肌和背部的力" +
                        "量，练就粗壮的手臂。但由于运动幅度只有一半，所以练习该动作之后，你需要补充一些完" +
                        "整幅度的动作，比如偏重引体向上或窄距引体向上。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "身体降得越低，引体向上就越难做。如果你还不能很好地掌握单臂半引体向上，那还是" +
                        "先集中练习该动作的靠上阶段，即身体接近横杆时的幅度。久而久之，一点点地增加动作幅" +
                        "度，直到可以标准地做这个动作。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 4 , IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 11 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 8,IsSingle = 1},
                    },
                    new SkillStyle{
                        ID = 9,
                        SkillID = 3,
                        Name = "单臂辅助式",
                        VideoUri = "ms-appx:///skill_3_style_09.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_9_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_9_2"),
                        ActionDescription = "在横杆上搭一条毛巾，向上跳起，单手以最容易发力的姿势抓住横杆，悬挂的毛巾位于" +
                        "另一只手的那一侧。用那只手抓住毛巾，抓握位置要尽量低——对大多数人来说，与眼睛等" +
                        "高的位置比较合适。双膝弯曲，脚踩交叠在一起并置于身后。双肩收紧，抓握横杆的那只手" +
                        "臂要微微弯曲。这是该动作的起始姿势（图57）。然后将自己拉起来——前一半动作中，即" +
                        "从起始姿势到抓握横杆那只手臂弯曲成直角的过程中，另一只手都要拉毛巾以协助完成动" +
                        "作。此后放开毛巾，只用单手继续将自己向上拉，直到下巴高过横杆（图58）。暂停一下，" +
                        "然后仅用单手的力量放低身体。在动作的最低点时再抓住毛巾。暂停，再重复动作。",
                        Analysis = "单臂辅助引体向上是一个特殊练习，它的作用是帮助锻炼者“体验”单臂引体向上，又" +
                        "不至于使其卡在最低位置。该动作可以缓慢、安全并极大地增强训练者肌腿的力量——这是" +
                        "做真正的单臂引体向上所必需的.",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "抓握毛巾的那只手的位置越低，就越难提供助力。如果你不能做5 次反复，那就升高抓" +
                        "握毛巾的位置，使其更靠近横杆一点儿。等你变得更强以后，就可以降低抓握毛巾的位置。" +
                        "最后，你会感觉到自己是在向下“推”毛巾，而不是“拉”毛巾，这样你的身体就能在这个" +
                        "动作中变得越来越强，并为练习最终式——单臂引体向上——做好准备。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 3 , IsSingle =1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 5 , IsSingle =1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 7, IsSingle =1},
                    },
                    new SkillStyle{
                        ID = 10,
                        SkillID = 3,
                        Name = "单臂式",
                        VideoUri = "ms-appx:///skill_3_style_10.mp4",
                        Img1 = Utility.GetImage("Skill_3_Style_10_1"),
                        Img2 = Utility.GetImage("Skill_3_Style_10_2"),
                        ActionDescription = "向上跳起，单手以最容易发力的抓握姿势牢牢抓住高过头顶的横杆。双腿离地，双膝弯" +
                        "曲，脚躁交叠在一起并置于身后，以防止腿部摆动。另一只手可随意放置，舒服即可。（你" +
                        "在练习第八式单臂半引体向上时，应该已经找到了最舒服的姿势）。发力的肩部要收紧，同" +
                        "时全身绷紧准备做动作。你将要做的可是高级力量技巧，所以你需要在心理上也做好准备。" +
                        "发力那只手的手臂几乎伸直，但略有弯曲以减少关节的压力。这是该动作的起始姿势（图" +
                        "59）。弯曲肘部，夹起肩部，将自己拉起来，直到下巴高过横杆。在整个动作过程中，尽量" +
                        "少用惯性。这是该动作的结束姿势（图60）。暂停一下，再缓慢地放低身体，回到起始姿势。" +
                        "在最低处暂停一下，再重复动作—如果你行的话。",
                        Analysis = "不靠“蹬腿”的完整幅度的单臂引体向上，可能是最伟大的背部与手臂练习，它可以赋" +
                        "予你强悍的力量与块头。掌握单臂引体向上后，你的背阔肌就会像一对翅膀一般，上背部也" +
                        "会发展出如蟒蛇盘绕交错般的肌肉。此外，你的抓握力、上臂及前臂力量会比健身房里的那" +
                        "些寻常鼠辈不知强多少倍—你很可能在摔跤比赛中把那些所谓健美人士的手臂扯下来。",
                        TraningPart = "背部、肱二头肌",
                        SlowSteady = "这是一个艰苦卓绝的技巧，但如果你下定决心、身体力行，最终肯定能掌握它。但也别" +
                        "梦想一步登天，你需要时间和耐心，在前九式中好好地发展身体。你最初的目标是完成一次" +
                        "完美的单臂引体向上，当你做到了之后，再进行“巩固训练”（参见第249 页）。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 1 , IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 3 , IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 6, IsSingle = 1},
                    },
                },

            });

            SkillsTest.Add(new Skill
            {
                ID = 4,
                Name = "举腿",
                Description = "魔鬼六块腹肌",
                Img = Utility.GetImage("series4"),
                Styles = new ObservableCollection<SkillStyle> {
                    new SkillStyle{
                        ID = 1,
                        SkillID = 4,
                        Name = "坐姿屈膝式",
                        VideoUri = "ms-appx:///skill_4_style_01.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_1_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_1_2"),
                        ActionDescription = "坐在椅子或床的边缘，身体略微向后倾斜，双手抓住边沿，两腿伸直，双脚并拢，脚跟" +
                        "距离地面几厘米。这是该动作的起始姿势。平缓地抬起膝盖，直到膝盖距胸部约 15 一 25 厘米。在此过程中呼气，动作完成时呼气结束，腹肌保持收缩状态。这是该动作的结" +
                        "束姿势。暂停 1 秒钟，进行反向运动并回到起始姿势。伸展膝盖的同时吸气。双脚" +
                        "应该始终沿着一条直线移动，而且始终保持悬空，直到一组动作完成方可接触地面。腹部要" +
                        "始终收缩，动作要慢，要抵制快速完成动作的冲动。如果需要，可以在两次动作之间喘几口" +
                        "气（所有中段练习都一样）。",
                        Analysis = "对初学者来说，坐姿屈膝是理想的腹部练习动作，因为该动作可以培养良好的脊柱姿势，" +
                        "锻炼腹部肌肉，增强髋部屈肌。对大多数人来说，这个动作也相对容易，因此给他们提供了" +
                        "发展完美技巧的绝佳机会，为以后的中段练习做好准备。切记，动作要平缓，呼吸节奏要正" +
                        "确，腹部始终要保持收紧状态。",
                        TraningPart = "腹部",
                        SlowSteady = "坐姿屈膝动作的起始姿势（两腿伸展）与结束姿势（两膝靠近胸部）难度相当。想要降" +
                        "低坐姿屈膝的难度，就要在这两个难点之间适当缩小动作幅度。随着训练者的腰部越来越强，" +
                        "再逐渐加大动作幅度，直至动作完美无缺。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 25 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 40},
                    },
                    new SkillStyle{
                        ID = 2,
                        SkillID = 4,
                        Name = "平卧抬膝式",
                        VideoUri = "ms-appx:///skill_4_style_02.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_2_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_2_2"),
                        ActionDescription = "平躺在地上，双腿并拢，双手置于身体两侧的地板上。膝盖弯曲近 90°，双脚距离地" +
                        "面约 2 一 5 厘米。双手用力向下按压地板，这样有助于保持身体稳定。这是该动作的起始姿" +
                        "势（图 63）。然后平缓地抬起膝盖，越过髋部，直到大腿与地面垂直、小腿与地面平行，整" +
                        "个过程中膝盖始终接近 900。在此过程中呼气，腹部肌肉保持收紧。这是该动作的结束姿势" +
                        "（图 64）。暂停 1 秒钟，进行反向动作。降低双脚，回到起始姿势，并在此过程中吸气。在" +
                        "整组练习中双脚都不能接触地面。",
                        Analysis = "掌握坐姿屈膝之后，接着练习平卧抬膝可以进一步强化训练者的腰部。平卧抬膝训练脊" +
                        "椎肌肉、腹部肌肉、腹斜肌和腹横肌，使之协调工作，同时也能够锻炼大腿前侧肌肉。由于" +
                        "该动作要求训练者平躺在地上，所以髋部屈肌也必须更多地参与其中，这会使训练者适应之" +
                        "后强度更大的平卧动作和悬垂动作。",
                        TraningPart = "腹部",
                        SlowSteady  = "平卧抬膝的难点是要保持双脚离地。如果这对你来说有点儿困难，那就在两次动作之间" +
                        "让双脚接触地面。当你有力量在双脚离地的情况下连续做几次动作时，就要尽量抬起脚—即" +
                        "便只能做 2 次也应如此。久而久之，在两脚离地的前提下不断增加动作次数。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 20 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 35},
                    },
                    new SkillStyle{
                        ID = 3,
                        SkillID = 4,
                        Name = "平卧屈举式",
                        VideoUri = "ms-appx:///skill_4_style_03.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_3_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_3_2"),
                        ActionDescription = "平躺在地上，双腿并拢伸展，双手置于身体两侧的地面上。双腿抬起，膝盖弯曲，使大" +
                        "腿与小腿的夹角大约成135°，双脚距离地面约2 一5 厘米。这是该动作的起始姿势（图65）。" +
                        "该练习前半部分的动作包括平缓地抬起双腿和双脚，持续大约2 秒钟，直到双脚位于骨盆正" +
                        "上方（图66）。在整个动作过程中，膝盖弯曲的角度应该保持不变——始终“锁定”。双手" +
                        "向下按压地板，这样有助于保持身体稳定。两脚位于骨盆正上方时，略作停顿，然后进行反" +
                        "向动作。在回复到起始姿势时也略作停顿，然后重复以上过程。双脚向上运动时呼气，向下" +
                        "运动时吸气。在整组练习中，腹部始终都要收紧，双脚始终不能接触地面。",
                        Analysis = "平卧屈举腿是平卧抬膝的简单延续。双脚与身体距离更远，这会增加动作的难度，也会" +
                        "加大髋部、腰部以及腹部肌肉所受的压力，使它们变得更加有力。",
                        TraningPart = "腹部",
                        SlowSteady  = "平卧抬膝要求膝盖弯曲成90°，平卧屈举腿则要求膝盖弯曲成135°。弯曲角度越大，" +
                        "力臂越长，难度就越大。如果你还达不到该动作的初级标准，那就让膝盖再弯曲一点儿，略" +
                        "大于90°即可。等你变得更强之后，再一点点把腿伸直，直到达到135°的标准。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 30},
                    },

                    new SkillStyle{
                        ID = 4,
                        SkillID = 4,
                        Name = "平卧蛙举式",
                        VideoUri = "ms-appx:///skill_4_style_04.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_4_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_4_2"),
                        ActionDescription = "先做第三式前半部分的屈举腿动作，但在最高点的时候（图66）不要停顿，而是要完" +
                        "全伸直双腿，使其与地面垂直，并与上半身的夹角成90°。这是该动作的结束姿势（图67）。" +
                        "应该在这个两部分的动作过程中呼气。大多数中段练习此时都要反向重复前半部分的动作，" +
                        "但这个练习有所不同。在有阻力（重力）的情况下降低双腿比抬起双腿要容易，蛙举腿正利" +
                        "用了这一点。降低双腿并保持完全伸展（图68），直到双腿距离地面约2-5 厘米（图69）。" +
                        "大多数练习动作中上与下的过程都要经过2 秒钟，但该练习的下落过程要经过4 秒钟，以便" +
                        "身体在有利的姿势中获得更多的锻炼。双腿慢慢下降时吸气，然后重复以上动作。",
                        Analysis = "不管是平卧姿势，还是悬垂姿势，从屈举腿到直举腿的过渡都是相当大的挑战，需要更" +
                        "多的柔韧性和力量。蛙举腿能够帮助训练者渡过这个难关，其作用就像是屈举腿与直举腿之" +
                        "间的摆渡人，因为它能够锻炼腘绳肌和背郡勘力量与柔韧性。可惜的是，蛙举腿在健身界少" +
                        "有耳闻——20 世纪60 年代之后似乎就已失传，因为那时举腿练习已经被卷腹取代，不再流" +
                        "行。",
                        TraningPart = "腹部",
                        SlowSteady  = "如果你发现这个动作对你来说有些吃力，那就集中在靠上的动作幅度内，即不要把双腿" +
                        "放得太低。随着你力量的增加，再慢慢加大动作幅度，直到可以练习完整的动作。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 25},
                    },
                    new SkillStyle{
                        ID = 5,
                        SkillID = 4,
                        Name = "平卧直举式",
                        VideoUri = "ms-appx:///skill_4_style_05.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_5_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_5_2"),
                        ActionDescription = "平躺在地上，面部朝上。双脚并拢，双腿伸直，双手置于身体两侧。抬起双脚，使其距" +
                        "离地面约2-5 厘米。双手向下按压地板，以保持身体稳定。这是该动作的起始姿势（图70）。" +
                        "双腿锁定，抬起双脚直到它们到达骨盆正上方，即双腿与上半身的夹角成90°。这是该动" +
                        "作的结束姿势（图71）。抬脚的过程中呼气，腹部保持收紧。至少要用2 秒钟平缓地完成该" +
                        "动作，不要用猛劲。停顿片刻，然后反向动作，降低双腿的过程中吸气。到达起始姿势后略" +
                        "作停顿，再重复。在整个动作过程中，膝盖始终要锁定，双脚不能接触地面，直到一组完成" +
                        "之后方可。",
                        Analysis = "这个动作在军事训练营和武术学校中颇受欢迎，因为它既可以增加腹部和髋部的力量及" +
                        "耐力，又可以提升身体的运动能力和柔韧性。该动作貌似很简单，但那只是假象：只要膝关" +
                        "节稍微弯曲，两脚触地，借助惯性离开地板，该动作就会变得非常容易；可是，这也会使练" +
                        "习成效大打折扣。",
                        TraningPart = "腹部",
                        SlowSteady  = "只要膝盖稍微弯曲，就可以大幅降低动作难度，但我不推荐大家这样做，因为该动作的" +
                        "主要益处就是来自于双腿伸直。如果你还达不到该动作的初级标准，那就回到平卧蛙举腿（第" +
                        "四式），等你能够达到3 组，每组各30 次的标准后再尝试该动作。如果那时你还觉得困难，" +
                        "那就保持双腿伸直，但是缩小动作幅度，集中做靠上部分的动作，随着力量的增加再逐渐增" +
                        "加动作幅度，把脚放得越来越低。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20},
                    },

                    new SkillStyle{
                        ID = 6,
                        SkillID = 4,
                        Name = "悬垂屈膝式",
                        VideoUri = "ms-appx:///skill_4_style_06.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_6_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_6_2"),
                        ActionDescription = "向上跳起，抓住高过头顶的横杆，双手与肩同宽。横杆要够高，以使身体悬垂时双脚依" +
                        "然离地，即使离地仅有一厘米。身体成一条直线，保持肩部收紧（参见第107 页）。这是该" +
                        "动作的起始姿势（图72）。平缓地抬起膝盖，直到双膝与骨盆处于同一高度，膝关节弯曲成" +
                        "90°，大腿与地面平行。在以上运动过程中呼气，同时保持收腹。这是该动作的结束姿势（图" +
                        "73）。暂停一下，然后反向运动，直到身体完全伸展。在此过程中吸气，然后重复练习。",
                        Analysis = "从这一式开始，训练者将开始举腿系列中更难的悬垂动作。在地板上练习时，训练者只" +
                        "需部分地克服重力，而现在必须完全克服重力。这种变化会在短时间内急剧增强髋部与身体" +
                        "中段的力量。此外，在横杆上悬垂，也增加了胸腔肌肉（如前锯肌与肋间肌，它们在手臂和" +
                        "腹部之间起连接作用）的活动性。因此，在横杆上进行悬垂举腿练习比在双杠或类似设备上" +
                        "练习效果更好。",
                        TraningPart = "腹部",
                        SlowSteady  = "如果你还不能严格按照要求做5 次悬垂屈膝，那就减小动作幅度，集中于动作的靠上阶" +
                        "段，然后再逐渐增加幅度。不管做什么动作，都不要使用惯性。在该系列的前几式使用平缓" +
                        "的、完全有控制的动作，能够很好地锻炼肌肉与肌键的力量，这对你掌握后面的练习至关重" +
                        "要。切记，惯性无益。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 15},
                    },
                    new SkillStyle{
                        ID = 7,
                        SkillID = 4,
                        Name = "悬垂屈举式",
                        VideoUri = "ms-appx:///skill_4_style_07.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_7_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_7_2"),
                        ActionDescription = "向上跳起，抓住高过头顶的横杆。身体成一直缪星叙脚离地。双手大致与肩同宽，肩部" +
                        "收紧。弯曲膝盖，直到膝关节大约成135°，双脚置于身后几厘米。这是该动作的起始姿势" +
                        "（图74）。以髋部为轴，平缓地抬起双腿，直到双脚与骨盆在一个高度上。这是该动作的结" +
                        "束姿势（图75）。暂停一下，然后做反向动作，如此重复。整个运动过程中，只能移动髋部，" +
                        "膝盖要保持锁定。举腿时呼气，下降时吸气，始终保持收腹。",
                        Analysis = "悬垂屈举腿是悬垂屈膝的延伸，难度更大。悬垂屈膝时膝盖要弯曲成90°，而悬垂屈" +
                        "举腿时膝盖则要成135°。增长的力臂使得悬垂屈举腿成为到目前为止最难的中段练习。你" +
                        "的腹肌、腰部肌肉、前锯肌、髋部屈肌都会变得更强壮。",
                        TraningPart = "腹部",
                        SlowSteady  = "最初，你可能很难在整个动作过程中都将膝关节锁定在135°。双腿下降时，你很想把" +
                        "双腿伸直一点儿。要尽量避免这一趋势，因为在此过程中重新调整膝关节的角度会产生惯性，" +
                        "从而导致身体摇晃。如果你做该动作有困难，只要减少膝盖的弯曲角度即可—更接近90°。" +
                        "随着在每次练习后力量的不断提高，再逐渐增加膝盖的弯曲角度，直到最终成135°。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 15},
                    },
                    new SkillStyle{
                        ID = 8,
                        SkillID = 4,
                        Name = "悬垂蛙举式",
                        VideoUri = "ms-appx:///skill_4_style_08.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_8_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_8_2"),
                        ActionDescription = "起始姿势与第七式悬垂屈举腿相同（图74），就像做第七式一样，抬起双腿。双脚与髋" +
                        "部在同一高度时（图75），再将双脚伸向正前方，直至双腿完全伸直。这时双腿与地面平行，" +
                        "即与上半身之间的夹角成900（图76）。暂停一下，然后慢慢放下双腿，在此过程中双腿始" +
                        "终伸直（图77）。该动作完成时，身体完全伸展（图78）。然后回到起始姿势，重复练习。" +
                        "举腿时呼气，下降时吸气，腹部从始至终都要收紧。",
                        Analysis = "从力学上来说，举腿的下降过程比上举过程容易，悬垂蛙举腿便充分利用了这一点。努" +
                        "力练习该动作，可以非常快速地增加力量和柔韧性，从而使你能够更轻松地过渡到后面的直" +
                        "腿练习（第九式与最终式）。",
                        TraningPart = "腹部",
                        SlowSteady  = "如果你能达到悬垂屈举腿的升级标准，那做5 次悬垂蛙举腿应该不在话下。训练者难以" +
                        "从悬垂屈举腿升级到悬垂蛙举腿，通常是因为身体柔韧性较差，而非力量不足。其实这很容" +
                        "易解决：练习之前先做几分钟体前屈动作，拉伸下背部和腘绳肌。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 15},
                    },
                    new SkillStyle{
                        ID = 9,
                        SkillID = 4,
                        Name = "悬垂半举式",
                        VideoUri = "ms-appx:///skill_4_style_09.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_9_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_9_2"),
                        ActionDescription = "抓住高过头顶的横杆，身体成一条直线，双脚摺窦也，双肩收紧。双腿锁定，然后慢慢" +
                        "抬起，使之与地面之间的夹角成45°。这是该动作的起始姿势（图79）。膝关节保持锁定，" +
                        "然后平缓地抬起双腿，直到它们与地面平行。这是该动作的结束姿势（图80）。暂停一会，" +
                        "然后放下双腿，回到起始姿势，如此重复。举腿时呼气，下降时吸气，腹部保持收紧。",
                        Analysis = "膝盖锁定、不使用惯性做直举腿相当难，500 个认真的训练者里可能就有一个人能完成" +
                        "——没准更少。该动作如此困难的原因之一是动作幅度大（身体从完全伸展到完全直角的姿" +
                        "势）。训练者一旦获得在最高点将腿伸直（即悬垂蛙举腿的结果）所需的力量和柔韧性，就" +
                        "可以练习悬垂半举腿。与第八式相比，该动作使上半幅度的动作更难一些，并去除了下半幅" +
                        "度的动作。",
                        TraningPart = "腹部",
                        SlowSteady  = "如果你能达到悬垂蛙举腿的升级标准，那就意味着你能够完成更难的直腿动作（图80）。" +
                        "如果你觉得悬垂半举腿太难，那一定是因为你的肌肉力量还无法完成这么大的动作幅度。做" +
                        "出该动作的结束姿势（双腿伸直与地面平行），然后慢慢地、小幅度地向下再向上移动双腿，" +
                        "即便刚开始只能移动几厘米也没关系，随着力量的增加，你就能够动作标准地完成悬垂半举" +
                        "腿了。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 15},
                    },
                    new SkillStyle{
                        ID = 10,
                        SkillID = 4,
                        Name = "悬垂直举式",
                        VideoUri = "ms-appx:///skill_4_style_10.mp4",
                        Img1 = Utility.GetImage("Skill_4_Style_10_1"),
                        Img2 = Utility.GetImage("Skill_4_Style_10_2"),
                        ActionDescription = "现在你已经知道该怎么做悬垂直举腿了。抓住禅唾头顶的横杆，要保证身体悬垂时双脚" +
                        "依然离地。双手大致与肩同宽，双肩收紧。这是该动作的起始姿势（图81）。平缓地举起双" +
                        "腿，直到它们与地面平行，此过程至少要持续2 秒钟。举腿时呼气，尽量将所有气体都呼出" +
                        "肺部，使腹部完全收紧。这是该动作的结束姿势（图82）。暂停一下，然后反向运动，回到" +
                        "起始姿势，这一过程至少也要持续2 秒钟，在此过程中吸气。即使在起始姿势中腹部也要收" +
                        "紧，两腿始终锁定，整个运动过程中只用肌肉控制，不要用惯性。",
                        Analysis = "若根据上述规定严格练习，那么悬垂直举腿将是世界上最伟大的中段练习动作，卷腹、" +
                        "器械练习和负重仰卧起坐都相形见细。如果你能完美无缺地做上20 次悬垂直举腿，那你的" +
                        "腰部将无比强大和灵活，腹外斜肌、前锯肌、腹横肌和肋间肌将犹如岩石般坚硬突出，腹肌" +
                        "也会像钢板般结实。你将拥有魔鬼六块！",
                        TraningPart = "腹部",
                        SlowSteady  = "开始练习悬垂直举腿时，你应该已经很好地掌握了悬垂半举腿。如果还没有，那就不要" +
                        "贸然前行。如果已经掌握，那只需不断练习，慢慢增加动作幅度—即便每次只增加1 厘米—" +
                        "—你会在不知不觉之间征服这个动作。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 30},
                    },
                },

            });

            SkillsTest.Add(new Skill
            {
                ID = 5,
                Name = "桥",
                Description = "严阵以待的脊柱",
                Img = Utility.GetImage("series5"),
                Styles = new ObservableCollection<SkillStyle> {
                    new SkillStyle{
                        ID = 1,
                        SkillID = 5,
                        Name = "短桥式",
                        VideoUri = "ms-appx:///skill_5_style_01.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_1_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_1_2"),
                        ActionDescription = "躺在地上，双手叠放在腹部。膝盖弯曲，将双脚拉向臀部，直到胫骨与地面接近垂直，" +
                        "此时脚跟距离臀部约 15 一 20 厘米，脚掌平放在地上。双脚与肩同宽或略窄，依个人舒适度" +
                        "而定。这是起始姿势。然后双脚用力下压，身体向上拱起，使髋部和背部离开地面，" +
                        "直到仅以双肩和双脚支撑整个身体。此时，大腿和躯干应成一条直线，髋部不要下沉。这是" +
                        "结束姿势。暂停一会，然后做反向动作，缓缓地放低身体，直到回到起始姿势，如" +
                        "此重复。身体撑起时呼气身体放低时吸气",
                        Analysis = "用下肢来推动整个身体，这是开始脊柱训练最温和的方式。因为在日常生活中，我们通" +
                        "常都是通过腿部带动脊柱活动的，比如散步、弯腰等。在短桥的最高处保持躯干伸直的动作，" +
                        "会刺激脊椎和髋部的肌肉，同时几乎不会给脊椎骨造成任何压力。所以，对椎间盘有伤的人" +
                        "来说，这是极好的治疗动作。",
                        TraningPart = "脊椎",
                        SlowSteady  = "大多数人做短桥时都不会感觉太吃力。如果你正处于背伤恢复阶段，动作对你而言稍有" +
                        "困难，那你可以在锁部下方放上枕头或坐垫，以缩小动作幅度。",

                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 25 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 50},
                    },

                    new SkillStyle{
                        ID = 2,
                        SkillID = 5,
                        Name = "直桥式",
                        VideoUri = "ms-appx:///skill_5_style_02.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_2_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_2_2"),
                        ActionDescription = "坐在地上，双腿伸直，双脚与肩同宽。手掌平放在髋部两侧的地上，手指朝前。坐直，" +
                        "此时腿和上半身之间的夹角成90°。这是该动作的起始姿势（图85）。双手用力下压，双臂" +
                        "绷紧，同时将髋部向上推起，直到双腿与躯干成一条直线。下巴向上抬起，看向天花板，此" +
                        "时只用手掌和脚跟支撑身体。这是结束姿势（图86）。暂停一会，然后反向运动。身体撑起" +
                        "时呼气，身体放低时吸气。",
                        Analysis = "短桥主要通过双腿的推动活动脊椎肌肉，而直桥在此基础上又增加了手臂，再加上伸直" +
                        "整个身体的动作，从而增加了难度。直桥一方面会训练手臂，另一方面也会打开僵硬的身躯，" +
                        "并强化肩胛骨之间的肌肉，这对于难度更大的桥而言至关重要。",
                        TraningPart = "脊椎",
                        SlowSteady  = "如果你觉得上面描述的动作太难，那么可以缩短力臂，降低难度。像做短桥一样弯曲而" +
                        "不是伸直双腿做该动作（图84）。如果这样还是太难，那就双膝跪地、身体后仰做这个动作，" +
                        "将臀部向上抬起几厘米，使其离开小腿。一直这样练习，直到你足够强壮之后再尝试做标准" +
                        "的直桥。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 20 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 40},
                    },
                    new SkillStyle{
                        ID = 3,
                        SkillID = 5,
                        Name = "高低式",
                        VideoUri = "ms-appx:///skill_5_style_03.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_3_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_3_2"),
                        ActionDescription = "高低桥需要借助一个与膝盖等高或略高的物体产提监狱里，床铺是最好的选择。一般家" +
                        "庭里的床稍高一点，但也可以。坐在床的边缘，身体向后躺在床上，双脚平放地上，与肩同" +
                        "宽。身体往前挪，以便髋部离开床。双手放在头部两侧，手指指向脚。这是该动作的起始姿" +
                        "势（图87）。双手用力下压，肘部打开，推起髋部，同时背部弯起成弧形。继续平缓地尽力" +
                        "上推身体，至少让头部与身体完全离开床。手臂不必完全伸直，肘部应该是弯曲的。或许你" +
                        "只能将自己的身体推起几厘米，那就可以了。有控制地向后仰头，以便能看见身后的墙壁。" +
                        "这是该动作的结束姿势（图88）。然后反向运动，缓慢地放低身体，直到躯干与头部再次完" +
                        "全躺在床上。保持正常呼吸。",
                        Analysis = "高低桥是桥系列中第一个标准的“头手并列”姿势的练习。这种姿势会强化训练者的腕" +
                        "部，打开其肩部与胸部，为之后的动作打下基础。相比于之前的几式，此动作要求脊椎上部" +
                        "有更高的柔韧性和收缩力。",
                        TraningPart = "脊椎",
                        SlowSteady  = "角度越小（即头和手所处的位置越高），桥就越容易。如果床上的高低桥太难，那你可" +
                        "以尝试在更高的物体上练习，比如餐桌或书桌，直到可以用更低的物体练习为止。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 3, Number = 30},
                    },
                    new SkillStyle{
                        ID = 4,
                        SkillID = 5,
                        Name = "顶桥式",
                        VideoUri = "ms-appx:///skill_5_style_04.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_4_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_4_2"),
                        ActionDescription = "平躺在地上，弯曲膝盖，把脚拉向臀部，直到脚跟与臀部相距约15-20 厘米。双脚与肩" +
                        "同宽或略窄，双手撑在头部两侧的地板上，手指指向脚，两肘指向天花板。尽力抬起髋部，" +
                        "使身体离地。手臂与腿继续用力推，直到背部形成优美的弧形，髋部高高抬起。头向下仰，" +
                        "头顶指向地板，这是“桥式”。保持这个姿势一会儿，然后弯曲手臂与双腿，直到头部轻轻" +
                        "接触地板。这是该动作的起始姿势（图89）。再次暂停一会，然后将背部向上推起成“桥式”。" +
                        "这是该动作的结束姿势（图90）。运动过程中要小L，以免撞到头部。在整个练习组中，背" +
                        "部始终要保持弧形，并且尽量正常呼吸。完成训练目标之后，慢慢地放低肩部、背部和髋部，" +
                        "直至整个身体接触地面。",
                        Analysis = "瑜伽是以静态姿势训练人体的背部，“老派”体操则专注于动态的力量。这种小幅度练" +
                        "习只是学习完整的桥的预备阶段。",
                        TraningPart = "脊椎",
                        SlowSteady  = "如果你刚开始进人“桥式”有点儿吃力，那就在腰下面放一些东西——两三个坐垫或枕" +
                        "头应该就够了。如果你不能让头部接触地板，那就先练习较小的动作幅度，在以后的锻炼中" +
                        "再把头降得越来越低。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 25},
                    },
                    new SkillStyle{
                        ID = 5,
                        SkillID = 5,
                        Name = "半桥式",
                        VideoUri = "ms-appx:///skill_5_style_05.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_5_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_5_2"),
                        ActionDescription = "这个动作需要借助一个篮球或足球来控制动作幅度。坐在地上，把球放在自己身后的地" +
                        "上（靠近自己）。向后躺，只有双肩和双脚在地面上，双脚与肩同宽或略窄，球支撑着腰部。" +
                        "如果你感觉这种姿势不舒服，在开始前可以在球上放上毛巾或坐垫。双手撑在头部两侧的地" +
                        "板上，手指指向脚。然后，用手把双肩和头部推离地板，只用双脚、球和手掌支撑身体。这" +
                        "是该动作的起始姿势（图91）。在这个姿势基础上髋部要尽力向上顶起，伸展手臂和双腿，" +
                        "抬起背部，直到背部完全离开球。继续向上运动，直到背部形成完全的弧形。这是结束姿势" +
                        "（图92）。在最高处暂停一会，然后慢慢放低身体，回到起始姿势。在一组动作过程中，后" +
                        "腰只能轻轻接触球，而不能将整个身体的重量都压在球上。重复练习，尽量保持正常呼吸。",
                        Analysis = "半桥其实是第六式标准桥的上半部分动作。等你能达到下面的升级标准后，你的脊椎肌" +
                        "肉将变得强劲且柔韧，足以完成更难的标准桥的下半部分动作。",
                        TraningPart = "脊椎",
                        SlowSteady  = "与大多数桥动作一样，如果你觉得如上述般完成训练目标有困难，可以先缩小动作幅度，" +
                        "然后一点点增加难度。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 8 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 15 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20},
                    },
                    new SkillStyle{
                        ID = 6,
                        SkillID = 5,
                        Name = "标准式",
                        VideoUri = "ms-appx:///skill_5_style_06.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_6_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_6_2"),
                        ActionDescription = "平躺在地上，弯曲膝盖，让双脚向臀部靠近，直至与其相距约15 一20 厘米。双脚与肩" +
                        "同宽或略窄，双手撑在头部两侧的地板上，手指指向脚，两肘指向天花板。这是该动作的起" +
                        "始姿势（图93）。尽量把髋部向上抬，从而使身体离开地板。手臂和双腿继续用力推，直到" +
                        "背部形成优美的弧形。在完美的桥中，手臂要完全伸直。头部尽量后仰，从而看到后面的墙" +
                        "壁。这是该动作的结束姿势（图94）。在最高处暂停一会儿，然后反向运动，有控制地放低" +
                        "身体——要平缓地降低，不要一下塌下来，这样你会受益更多。继续放低身体，直到髋部、" +
                        "背部和头部完全接触地面。这一连串动作就是一个标准的桥。完成相应的训练目标，整个过" +
                        "程尽量保持正常呼吸。",
                        Analysis = "标准桥是一个不同寻常的动作，它既可以预防和治疗很多背部问题，也可以增加全身的" +
                        "柔韧性，还可以增强脊柱深层肌肉的力量、扩展胸腔，舒展肩膀。此外，它还能强化双臂和" +
                        "双腿，改善血液循环，甚至有助于消化。",
                        TraningPart = "脊椎",
                        SlowSteady  = "想要做到理想的桥——尤其是将双臂和双腿完全伸展—确实很难，需要耐心刻苦的练" +
                        "习。开始时尽量把身体向高抬，“完美”终有一天会来眷顾你的。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 6 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 15},
                    },
                    new SkillStyle{
                        ID = 7,
                        SkillID = 5,
                        Name = "下行式",
                        VideoUri = "ms-appx:///skill_5_style_07.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_7_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_7_1"),
                        ActionDescription = "站在距墙壁大约一臂远的位置，如果把握不准，可以稍近一点儿，这样可以更安全地调" +
                        "整。双脚与肩同宽，锁部向前挺，身体向后弯。抬起下巴，头尽量向后仰，以舒服为准。身" +
                        "体继续平缓地向后弯，直到可以看到身后的墙壁。一旦能看见墙壁，便将双手举过头顶，手" +
                        "掌紧贴墙壁，手指朝下，与头部齐平。这是该动作的起始姿势（图95）。将一部分体重向后" +
                        "转移到手上，把一只手降低几厘米，再让其紧贴墙壁；然后再移动另一只手，使其降得更低。" +
                        "用手在墙壁上向下“行走”时，身体要一直向后弯曲（图96）。手向下移动的同时，双脚也" +
                        "要一点点远离墙壁，以适应身体弯曲—只要感觉需要这样做.就小步向前移动。双手继续交" +
                        "替下移，直到移至墙根为止。之后双手手掌撑地，此时你其实是在墙根处做标准的桥式。这" +
                        "是该动作的最低点（图97）。然后让身体落到地板上，再站起来，回到起始姿势，重新开始" +
                        "下一轮动作。整个动作过程中要平缓呼吸。",
                        Analysis = "顺着墙璧向下“走”比向上“走”容易，所以要先掌握这个。",
                        TraningPart = "脊椎",
                        SlowSteady  = "第一次做该动作就能“走”到墙根的人寥寥无几。你可以一点点地增加动作幅度——每" +
                        "次训练都降得更低一点儿。另外，“把步子迈得小一些”也会更容易一些。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number =3 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 6 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 10},
                    },
                    new SkillStyle{
                        ID = 8,
                        SkillID = 5,
                        Name = "上行式",
                        VideoUri = "ms-appx:///skill_5_style_08.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_8_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_8_1"),
                        ActionDescription = "背向墙壁（不接触墙壁）站立，身体向后弯曲，双手举过头顶，与墙壁接触，做出第七" +
                        "式的起始姿势（图95）。然后，如第七式中描述的那样，双手向下“行走”，直到贴着墙根" +
                        "做出标准的桥式（图97）。接下来，你需要向反方向运动。让一只手重新接触墙壁，同时用" +
                        "力推墙壁，之后让另一只手也接触墙壁，位置比之前那只手略高（图98）。把双手从地面上" +
                        "转移到墙壁上，是该动作最难的地方。接来下只是交替地把一只手放得比另一只手更靠上一" +
                        "些，顺着墙壁往上“走”。随着身体逐渐伸展，你很可能需要慢慢小步移动向墙壁靠拢，以" +
                        "保证手掌处有足够的压力。继续向上“走”，直到身体几乎伸直（图99）。然后，双手轻轻" +
                        "推墙，再次完全脱离墙壁站立（图100）。站立、向下“行走”、向上“行走”、再回到站立" +
                        "姿势，这就是一个完整的动作。",
                        Analysis = "一旦你具备了顺着墙壁向下“走”所需要的柔韧性与力量，就该开始练习向上“走”了。" +
                        "这并不要求更好的柔韧性，但要求更大的力量，因为你要克服重力。",
                        TraningPart = "脊椎",
                        SlowSteady  = "与第七式一样，想要完美地做成该动作，关键在于逐渐增加动作幅度。首次尝试时，只" +
                        "要顺墙壁向下“走’到某一点就可以了—确保你还能从这一点再向上“走”回来。如果觉得" +
                        "有用，就用粉笔把这一点标记下来，并逐渐降低下降的高度。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 2 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 4 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 8},
                    },
                    new SkillStyle{
                        ID = 9,
                        SkillID = 5,
                        Name = "合式",
                        VideoUri = "ms-appx:///skill_5_style_09.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_9_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_9_1"),
                        ActionDescription = "直立，双脚与肩同宽，身体后方的空地要足以让训练者平躺。这是该动作的起始姿势。" +
                        "双手置于镜部两侧，并开始向前推骨盆（图101）。当骨盆移至你的极限时，开始弯曲膝盖，" +
                        "同时脊柱向后弯曲成弓形。然后头部向后仰，眼睛向后看。整个过程要流畅、一气呵成。继" +
                        "续弯曲脊柱，直到你可以看到身后几厘米的地面。一旦看到地面，就让双手离开髋部并将其" +
                        "举过头顶（图102）。这种姿势要求你有很好的柔韧性，前移的髋部加上弯曲的膝盖能够防" +
                        "止你向后摔倒。继续向后、向下运动，手臂保持伸展，直到手掌接触地面。这是该动作的结" +
                        "束姿势（图103），标准的桥式。接着，弯曲手臂与双腿，直到背部着地。然后站起来，回" +
                        "到起始姿势，重复动作。在整个练习组中，保持正常呼吸。",
                        Analysis = "该动作是目前为止难度最高的桥。它包含最终式铁板桥中的“离心”运动阶段，即反向" +
                        "运动阶段。",
                        TraningPart = "脊椎",
                        SlowSteady  = "起初，在该动作的最后三分之一阶段，你的身体很可能会向后倒，运气好的话你可能还" +
                        "能够用手掌“砸”地。这样可不行，你必须反复练习，直到你可以让双手温和地落在地面上。" +
                        "有个小窍门对你会有帮助，就是向后倒在台阶上—每次选择更低的台阶，直到可以让双手缓" +
                        "和地落在地上。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 1 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 3 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 6},
                    },
                    new SkillStyle{
                        ID = 10,
                        SkillID = 5,
                        Name = "铁板式",
                        VideoUri = "ms-appx:///skill_5_style_10.mp4",
                        Img1 = Utility.GetImage("Skill_5_Style_10_1"),
                        Img2 = Utility.GetImage("Skill_5_Style_10_1"),
                        ActionDescription = "直立，按照合桥（第九式）的运动过程，做出标准桥式（图104）。双臂伸直，同时弯" +
                        "曲膝盖，将体重转移至双腿。双手（最后是手指）用力按压地面，使手掌离地，同时继续向" +
                        "前转移体重。此时，如果你的后背足够柔韧以维持高度的弓形，腹部又足够有力，在你起身" +
                        "时手指会离地（图105）。这个向上的运动过程应该是平缓向前转移体重的结果，而非用双" +
                        "手以爆发力推地面的结果。继续向上运动，双手绕过肩部收回，颈部也收回，与身体成一直" +
                        "线。最后，将髋部拉回，成直立姿势，双手回到身体两侧。这是该动作的结束姿势（图106）。" +
                        "从直立姿势做成标准桥式，然后把自己从下向上拉回成直立姿势，就是一次完整的动作。重" +
                        "复练习，保持正常呼吸。",
                        Analysis = "这是桥系列的最终式，它需要不可思议的柔韧性、强大的关节、强有力的肌肉以及极好" +
                        "的平衡能力和协调能力。经常练习铁板桥，可以增强敏捷性、按摩内部器官，调整脊椎和肌" +
                        "肉系统，让你精力充沛。达到高次数练习后，它还会为你的新陈代谢“加油”",
                        TraningPart = "脊椎",
                        SlowSteady  = "犹如练习合桥（第九式）一样，练习铁板桥也可以借助台阶一点点增加动作幅度。采用" +
                        "较宽的站姿也会有所帮助，但最终还是要争取回归到与肩同宽的站姿。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 1 },
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 3 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 30},
                    },
                },

            });

            SkillsTest.Add(new Skill
            {
                ID = 6,
                Name = "倒立撑",
                Description = "强有力的肩膀",
                Img = Utility.GetImage("series6"),
                Styles = new ObservableCollection<SkillStyle> {
                    new SkillStyle{
                        ID = 1,
                        SkillID = 6,
                        Name = "顶墙式",
                        Img1 = Utility.GetImage("Skill_6_Style_1_2"),
                        Img2 = Utility.GetImage("Skill_6_Style_1_2"),
                        //动作
                        ActionDescription = "找一堵墙，在墙根处放置一个枕头（坐垫或叠好的毛巾也可）。双手和双膝着地，将头" +
                        "顶在枕头上，头部距离墙壁约 15 一 25 厘米。双手稳稳地放在头部两侧，大约与肩同宽。抬" +
                        "起一条腿的膝盖，让其靠近同侧的肘部，同时伸直另一条腿，使膝盖离地（图 107）。然后，" +
                        "让靠近肘部的腿使劲蹬地，同时将另一条腿向上踢，从而让两条腿同时靠向墙壁。一旦双脚" +
                        "靠在墙壁上，就慢慢伸直双腿，把身体摆正（图 108）。嘴巴保持闭合，用鼻子平缓呼吸。" +
                        "坚持所需时间后，弯曲双腿，并有控制地放下它们。",
                        //解析
                        Analysis = "任何想做倒立撑的人，首先都必须掌握倒立姿势。靠墙顶立是完美的人门技巧，只需稍" +
                        "加练习，我们的血管、内脏器官以及头部就会适应这种突然的颠倒。在这个动作中，整个身" +
                        "体都在头部上方，这对身体的平衡能力是一个考验。肩部要维持身体的平衡，因此也会得到" +
                        "一定的锻炼。",
                        //训练部位
                        TraningPart = "肩部",
                        //稳扎稳打
                        SlowSteady = "在做靠墙顶立时，大多数人都能坚持几秒钟，主要问题是如何做成倒立姿势。难就难在" +
                        "找到上墙所需的推和蹬的正确力道。如果你觉得这有点儿困难，可以先请朋友帮忙，直到最" +
                        "终可以独立完成。",
                        //初级组
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 30 ,TraningType = 1},
                        //中级
                        IntermediateStandard = new Standard{GroupsNumber = 1, Number = 60 ,TraningType = 1},
                        //升级
                        UpgradeStandard = new Standard{ GroupsNumber = 1, Number = 120,TraningType = 1},
                    },

                    new SkillStyle{
                        ID = 2,
                        SkillID = 6,
                        Name = "乌鸦式",
                        Img1 = Utility.GetImage("Skill_6_Style_2_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_2_1"),
                        ActionDescription = "双膝分开，呈蹲坐姿势。双手手掌放在身体前面的地板上，与肩同宽。双臂略微弯曲，" +
                        "身体向前倾斜，然后让双膝稳稳地夹在两肘外侧（图109）。身体继续前倾，一点一点把体" +
                        "重转移到手掌上，双脚的负重则越来越少。最终重心前移，双脚离地。双脚用力提起，保持" +
                        "平衡，平缓呼吸，坚持一定的时间（图110）。然后反向运动，身体重心慢慢后倾，直到脚" +
                        "尖再次接触地面。",
                        Analysis = "乌鸦式将教你结合手臂和肩部的力量使身体达到平衡，这是进人倒立撑练习的必要一" +
                        "步，因为你要通过手臂来平衡全身的重量。第一式已经使你在倒立时更加自在，第二式则能" +
                        "够帮助你发展肩部、腕部和手指的基本“平衡”力量。由于这不是倒立姿势，所以练习该动" +
                        "作之后，要接着练习靠墙顶立，才能获得力量训练与倒立平衡的全部效果。",
                        TraningPart = "肩部",
                        SlowSteady = "这个动作的关键是找到独特的平衡点。在这个姿势中保持平衡的艺术在于利用微妙的手" +
                        "指力量阻止身体向前倾倒（在用手平衡的更高级的动作中也都一样）。如果你开始向前倾，" +
                        "那就要使劲下压手指。腿要抬得足够高，以防身体向后落。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 10 ,TraningType = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 1, Number = 30 ,TraningType = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 1, Number = 60,TraningType = 1},
                    },
                    new SkillStyle{
                        ID = 3,
                        SkillID = 6,
                        Name = "靠墙式",
                        Img1 = Utility.GetImage("Skill_6_Style_3_2"),
                        Img2 = Utility.GetImage("Skill_6_Style_3_2"),
                        ActionDescription = "找一面墙，双手手掌平放在距离墙根约15-25 厘米的地面上，双手与肩同宽。手臂伸直" +
                        "或近乎伸直，膝盖弯曲，撑起身体。提起一条腿的膝盖，让其靠近同侧的肘部（图111），" +
                        "然后使劲向下蹬地，同时让另一条腿向后上方摆。与此同时，让蹬地的腿也离地，紧随另一" +
                        "条腿向墙壁靠近，手臂保持伸展，双脚脚跟应同时接触墙壁。刚开始练习时，上踢的力量如" +
                        "果过大，你的后背和屁股会猛地撞到墙上，但久而久之你就会掌握完美的上墙技艺。最后，" +
                        "你的手臂应该是直的，身体摆正，背部略向内弓。这就是标准的靠墙倒立姿势（图112）。" +
                        "保持这一姿势一段时间，整个过程中保持正常呼吸。",
                        Analysis = "靠墙顶立应该已经让你适应了上下颠倒，乌鸦式应该已经使你的手臂和腕部获得力量来" +
                        "安全地通过双手平衡全部的体重。掌握这些动作之后，接下来就需要学习蹬起靠墙成标准倒" +
                        "立姿势的技艺，这比蹬起成顶立姿势更难（因为手臂要完全伸展）。这个动作不仅能教你这" +
                        "个重要技巧，还会增加你的肩部的基础万量。",
                        TraningPart = "肩部",
                        SlowSteady = "如果你练过蹬起成靠墙顶立（第一式），那么这个动作对你来说应该不会太难，只不过" +
                        "在蹬起时需要更加用力。如果刚开始感觉这点有些困难，可以尝试将双脚踩在某个东西（比" +
                        "如盒子或椅子）上蹬起。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 30 ,TraningType = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 1, Number = 60 ,TraningType = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 1, Number = 120,TraningType = 1},
                    },
                    new SkillStyle{
                        ID = 4,
                        SkillID = 6,
                        Name = "半倒立式",
                        Img1 = Utility.GetImage("Skill_6_Style_4_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_4_1"),
                        ActionDescription = "找一堵墙，双手手掌平放在距离墙根约15 一25 厘米的地面上，双手与肩同宽。手臂尽" +
                        "量伸直，蹬起成靠墙倒立姿势（第三式）。你现在应该处于标准的靠墙倒立姿势——手臂伸" +
                        "直，身体收紧，背部略微向内弓，脚跟与墙壁轻轻接触。这是该动作的起始姿势（图113）。" +
                        "然后弯曲肘部，使头部向地面方向下降一半高度。这是该动作的结束姿势（图114）。暂停" +
                        "一下，然后稳稳地推起身体，回到起始姿势。整个动作的运动幅度大约只有15 厘米。刚开" +
                        "始练习时不要误判距离，让身体降得太低。整组练习中保持平缓呼吸。",
                        Analysis = "在靠墙倒立（第三式）的静止姿势中，你的肩部、肘部和躯干应该已经获得了一些力量。" +
                        "该动作的强度要大得多，能够强化整个上肢带肌，并培养强劲有力的肘部和肪三头肌，同时" +
                        "对胸肌上部也有好处。",
                        TraningPart = "肩部",
                        SlowSteady = "通过之前几式的练习，你应该已经学会如何顺利蹬起靠墙成倒立姿势了。但是半倒立撑" +
                        "还要求上半身必须有极好的力量，如果上述动作对你而言太吃力，那你就要减小动作幅度。" +
                        "开始时只是稍微弯曲手臂—可能身体只下降了几分之一厘米，然后逐步增加次数和动作幅" +
                        "度，直到头部可以向地面下降一半的距离。假以时日，你会做到的。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,TraningType = 0},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 ,TraningType = 0},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 20,TraningType = 0},
                    },
                    new SkillStyle{
                        ID = 5,
                        SkillID = 6,
                        Name = "标准式",
                        Img1 = Utility.GetImage("Skill_6_Style_5_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_5_1"),
                        ActionDescription = "找一面墙，双手手掌平放在距离墙根约15 一25 厘米的地面上，双手与肩同宽。双膝曲，" +
                        "蹬起靠墙成倒立姿势。如果你从前几式一路练过来，那现在对此过程必然了如指掌。如果你" +
                        "已经找到了适合自己的上墙技巧，那也很好。重要的是培养肌肉，而不是上墙的方式。上墙" +
                        "之后，只有双脚脚跟与墙壁接触，背部略微向内弯曲成弓形，双臂伸直。这是该动作的起始" +
                        "姿势（图1 巧）。弯曲肘部，直到头顶轻轻接触地板。这是该动作的结束姿势（图116）。使" +
                        "用“亲亲宝贝”的方法保护头部（参见第37 页）。暂停1 秒钟，然后推起身体，回到起始姿" +
                        "势。在所有倒立动作中都要通过肌肉控制身体，同时还要集中精神，以确保安全。尽量保持" +
                        "平缓呼吸。",
                        Analysis = "这是标准的“囚徒”倒立撑，它可以强化肩部、肮三头肌、肘部、斜方肌、胸肌及双手" +
                        "—其实整个上半身的力量都会得到发展。很多训练者都认为，倒立撑应该是“自由”的，即" +
                        "离开墙做。但这对平衡能力的要求极高。所有老派倒立练习者都相信，要想拥有超凡的平衡" +
                        "能力，首先应该培养力量。",
                        TraningPart = "肩部",
                        SlowSteady = "最低点是该动作的最难点。如果你不能完成5 次全幅度的倒立撑，那就不要一开始就降" +
                        "到最低点。等你更强壮的时候再增加动作幅度。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 10 },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 15,},
                    },
                    new SkillStyle{
                        ID = 6,
                        SkillID = 6,
                        Name = "窄距式",
                        Img1 = Utility.GetImage("Skill_6_Style_6_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_6_1"),
                        ActionDescription = "找一面墙，双手手掌平放在距离墙根约15 一25 厘米的地面上，但双手（尤其是两个食" +
                        "指）要互相接触。蹬起成倒立姿势，双臂伸直，脚跟与墙壁接触，身体微微弯曲。这是该动" +
                        "作的起始姿势（图117）。弯曲肘部，直到头部轻轻“亲吻”地板，肘部保持向前、向外的" +
                        "朝向（图118）。暂停一下（此时完全在控制之中），然后推起身体，回到起始姿势。",
                        Analysis = "标准倒立撑是绝佳的基本练习，可以教你有力且协调地使用最强的推力肌肉。但是，如" +
                        "果你想升级到非常高级的单臂倒立动作，那就需要非常强大的肌腱，尤其是肘部、前臂及腕" +
                        "部的肌腱。窄距倒立撑可以培养这些肌腿的力量，因为双手接近的姿势使得上肢带肌在这个" +
                        "动作中更难发力，而这会强迫肘部变得更加强壮。",
                        TraningPart = "肩部",
                        SlowSteady = "如果你够强，在达到一个动作的升级标准后直接升级到下一式动作的初级标准，通常不" +
                        "会有太多问题。可是，在从标准倒立撑升级到窄距倒立撑时你最好慢慢来，以让肌腔逐步适" +
                        "应。掌握倒立撑之后，每次练习时（或在你觉得状态不错的时候）都让双手更靠近一点儿，" +
                        "在地板上做标记也许有帮助。如果做倒立撑时你的双手相距大约45 厘米，那你要想升级到" +
                        "窄距倒立撑，至少要用18 周，甚至更长的时间。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 9 , },
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 12,},
                    },
                    new SkillStyle{
                        ID = 7,
                        SkillID = 6,
                        Name = "偏重式",
                        Img1 = Utility.GetImage("Skill_6_Style_7_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_7_1"),
                        ActionDescription = "在墙边放一个篮球，用自己觉得最容易的方式在篮球旁边蹬起成靠墙倒立，然后将一只" +
                        "手伸出去放在篮球上。这个动作说来简单，实际做起来却非常难，这需要你在很短时间（你" +
                        "的手找篮球所用的时间）内用一只锁定的手臂支撑整个体重。手在篮球上放稳之后再调整球" +
                        "的位置，使双手间距大约与肩同宽。支撑在地上的那只手臂要伸直，另一只手臂则是弯曲的。" +
                        "让双手尽量均匀地承担体重，平缓呼吸。在这时，肪三头肌、肱二头肌及肩部都需要非常卖" +
                        "力，否则很可能因为控制不住篮球而摔倒。这是该动作的起始姿势（图119）。弯曲肘部，" +
                        "直到头部轻轻接触地板。这是该动作的结束姿势（图120）。暂停一下，然后推起身体。",
                        Analysis = "为了把自己推起来，必须稳住篮球。如果不能以静力控制篮球，篮球就会向外滚动，这" +
                        "需要有力量极大的手臂与肩部，还有冠军级的肩袖。征服这个动作，你就会拥有超级强悍的" +
                        "关节和山地大猩猩般的肩膀。",
                        TraningPart = "肩部",
                        SlowSteady = "使用篮球要求训练者有高度的力量与平衡能力，还有快速的反应能力。我建议所有训练" +
                        "者刚开始练习这个时都使用稳固的物体，而不是篮球。可以使用一块砖头来起步，到后来用" +
                        "三块垒起来的砖头。在监狱里，很多家伙刚开始练习时会把几本薄书摞起来，以后再逐步往" +
                        "上加书。等到书与篮球一样高时，再用篮球做尝试。总之，安全至上。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 5 ,IsSingle=1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 8 ,IsSingle=1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 10,IsSingle=1},
                    },
                    new SkillStyle{
                        ID = 8,
                        SkillID = 6,
                        Name = "单臂半式",
                        Img1 = Utility.GetImage("Skill_6_Style_8_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_8_1"),
                        ActionDescription = "蹬起靠墙成倒立姿势，脚跟与墙壁接触，身体略微成弓形。双手与肩同宽，距离墙根约" +
                        "15 一25 厘米，手臂伸直。逐渐抬起一只手的手掌，将重心慢慢转移到身体的另一侧，另一" +
                        "侧的手掌会承受越来越多的体重。继续这种转移过程（持续几秒钟），直到略微抬起的手掌" +
                        "上只剩下几千克的压力。现在轻轻抬起这只手，使之离开地面，并将它伸向远处以保持平衡。" +
                        "现在，你就是在以一只伸直的手臂支撑整个身体。这是该动作的起始姿势（图121）。支撑" +
                        "身体的手臂肘部弯曲，直到头部向地面方向下降一半高度。这是该动作的结束姿势（图122）。" +
                        "暂停一下，然后推起身体。",
                        Analysis = "这是以单臂推起整个体重的第一个动作，该动作不仅要求训练者有极大的肩部和手臂力" +
                        "量，也需要有极为强大的关节、较高的身体协调能力、出色的平衡能力以及极其熟练的倒立" +
                        "推举技巧。要想从单臂半倒立撑中获益，你得投人大量时间，“榨干”此前那些动作的全部" +
                        "“营养”—要花费至少六个月甚至更长的时间，否则连试都不要试这个，不然你很可能是在" +
                        "自讨苦吃。",
                        TraningPart = "肩部",
                        SlowSteady = "该动作很难，只有长久练习，逐渐增加动作幅度，才能掌握。试着以手掌推起身体，而" +
                        "不是用手指推，这样有助于推力肌肉协调发力。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 4 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 6 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 8,IsSingle = 1},
                    },
                    new SkillStyle{
                        ID = 9,
                        SkillID = 6,
                        Name = "杠杆式",
                        Img1 = Utility.GetImage("Skill_6_Style_9_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_9_1"),
                        ActionDescription = "蹬起靠墙成倒立姿势。像平常一样，双手距离与肩同宽，距离墙根约15 一25 厘米。只" +
                        "有双脚脚跟与墙壁接触，身体保持自然的弓形。如第八式那样，把身体的大部分重量（约" +
                        "90%）慢慢转移到一只手上。然后翻转另一只手的手掌，使手背贴地，手心向上，手指" +
                        "朝前（视线方向为前）。将这只手臂在自己面前伸展，而这只手始终保持与地面的接触——" +
                        "部分体重的压力仍然要通过这只手的手指传递。这是该动作的起始姿势（图123）。手掌向" +
                        "上的那只手臂保持伸展状态，另一只手臂的肘部弯曲，完全在肌肉的控制之中放低身体，不" +
                        "要让身体快速下落，否则会伤到头部，甚至很可能扭伤颈部。头顶轻轻接触地板，然后暂停" +
                        "一下。这是该动作的结束姿势（图124）。最后，用一只手的手掌及另一只手的手背同时推" +
                        "起身体，回到起始姿势。",
                        Analysis = "前一式将使你掌握单臂倒立撑的上半部分，这个动作则能够帮你掌握更具挑战的下半部" +
                        "分动作。由于有一只手的手掌向上，所以那只手臂很难用太多力，只能确保在动作最低点时，" +
                        "给你足够的帮助以推起身体—这会让肌肉的发展最大化。",
                        TraningPart = "肩部",
                        SlowSteady = "辅助的（即手掌向上的）手臂如果略微弯曲，并更靠近身体，则能够给你更好的辅助力" +
                        "量。随着你不断变强，逐渐伸直辅助的手臂。",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 3 ,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 4 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 2, Number = 6,IsSingle = 1},
                    },
                    new SkillStyle{
                        ID = 10,
                        SkillID = 6,
                        Name = "单臂式",
                        Img1 = Utility.GetImage("Skill_6_Style_10_1"),
                        Img2 = Utility.GetImage("Skill_6_Style_10_1"),
                        ActionDescription = "蹬起靠墙成倒立姿势，向一侧略微倾斜，直到仅用单臂支撑身体的全部重量，就像单臂" +
                        "半倒立撑（第八式）中那样。脚跟与墙壁接触，身体略微成弓形。这是该动作的起始姿势（图" +
                        "125）。弯曲支撑身体的那只手臂，直到头顶轻轻接触地面。另一只手臂则要随时做好准备，" +
                        "以便失误时可以助你一臂之力。这是该动作的结束姿势（图126）。在将身体推回到起始姿" +
                        "势的过程中，可能需要用些爆发力。为使身体脱离动作的最低点，你也可以向上蹬腿：双膝" +
                        "弯曲（脚跟依然要接触墙壁）并迅速伸直，这样可以增加一些向上的冲力。",
                        Analysis = "单臂倒立撑是锻炼肩部和手臂的终极动作。忘掉卧推吧——它只能给你带来伤痛和不" +
                        "幸。谨慎练习整个倒立撑系列，直至掌握最终式，你的力量会比你碰到的所有练卧推的都大" +
                        "—这里说的是纯粹、有用、能把人抓起来扔出去的力量。如果有一位体重达90 千克的男子" +
                        "做单臂倒立撑，那就相当于他用单臂推举起了近90 千克的哑铃；如果是双臂，就是近180" +
                        "千克的杠铃！你认识几个家伙能提起（更不用说推举）180 千克的重量呢？体操会让你毫发" +
                        "无伤地拥有这等力量，还有一副健康的肩膀。",
                        TraningPart = "肩部",
                        SlowSteady = "你必须逐渐增加动作幅度。其实，唯一能真正掌握此动作的方式，就是练上几年—或许" +
                        "三年，或许更久。你本来就是要再老上三年的，对吧？那为什么不让自己在那时变得超级强" +
                        "大呢？",
                        PrimaryStandard = new Standard{ GroupsNumber = 1,Number = 1,IsSingle = 1},
                        IntermediateStandard = new Standard{GroupsNumber = 2, Number = 2 ,IsSingle = 1},
                        UpgradeStandard = new Standard{ GroupsNumber = 1, Number = 5,IsSingle = 1},
                    },
                },
            }) ;

        }
    }
}