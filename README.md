# CheckString
获取包含全部所需条件的最短字符串

例如 string a = "abwfwcsgafgbcwawdfa";   string b = "abc";
最短字符串应该是a中的  "bcwa"

将条件字符串中按字符生成字典，key为字符，value为字符所在字符串中的位置的集合

然后将字典中内容结果进行排列

建立一个树结构TreeObject 将排列结果以树状方式显示

将树的所有分支进行筛选，选取差值最小的一组，即为最短字符串
