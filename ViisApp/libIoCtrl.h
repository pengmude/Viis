#ifndef _LIB_IOCTRL_H
#define _LIB_IOCTRL_H

typedef enum{
	GPIO_PIN1,
	GPIO_PIN2,
	GPIO_PIN3,
	GPIO_PIN4,
	GPIO_PIN5,
	GPIO_PIN6,
	GPIO_PIN7,
	GPIO_PIN8
}GPIO_INDEX;

typedef enum{
	GPIO_LOW_LEVEL,
	GPIO_HIGH_LEVEL
}GPIO_LEVEL;

typedef enum{
	SET_GPIO_IN,
	SET_GPIO_OUT
}GPIO_INOUT;

typedef enum{
	SET_MINUTE,
	SET_SECOND
}WDT_MODE;

struct __WDT_INFO{
	int nowayout;
	unsigned char mode;
	unsigned char time;
	unsigned char wdtinited;
	unsigned char enable;
};

//init system driver
/*
**函数：libIoCtrlInit
**函数说明:初始化驱动及导入SDK动态库
**参数：hlib 模块地址指针，bios_id 主板BIOS ID号，在主板BIOS setup中可以查看得到、
**返回值: 0 成功，非0失败 ，成功时并导出HMODULE的实例hlib
*/
extern "C" __declspec(dllexport) int libIoCtrlInit(HMODULE *hlib,char* bios_id);

//for gpio 
/*
**函数：setPinInOut
**函数说明:配置管脚的输入输出
**参数：hlib 模块地址指针，index 管脚序号（参看GPIO_INDEX枚举定义），state 管脚输入输出设置(参看GPIO_INOUT的枚举定义)
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int setPinInOut(HMODULE *hlib, GPIO_INDEX index, GPIO_INOUT state);

/*
**函数：getPinLevel
**函数说明:获取管脚的输入输出状态
**参数：hlib 模块地址指针，index 管脚序号（参看GPIO_INDEX枚举定义），curstate 导出的管脚输入输出状态,0为低电平，1为高电平
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int getPinLevel(HMODULE *hlib, GPIO_INDEX index, unsigned char* curstate);


/*
**函数：setPinLevel
**函数说明:设置管脚的输入输出状态
**参数：hlib 模块地址指针，index 管脚序号（参看GPIO_INDEX枚举定义），curstate 设置管脚输入输出状态,0为低电平，1为高电平(仅为寄存器状态，实际电路的状态，取决于外部电路)
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int setPinLevel(HMODULE *hlib, GPIO_INDEX index, GPIO_LEVEL curstate);

//for watchdog
/*
**函数：cfgWatchdog
**函数说明:设置看门狗
**参数：hlib 模块地址指针，iwdtinfo 看门狗状态定义，请参看struct __WDT_INFO的定义
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int cfgWatchdog(HMODULE *hlib, struct __WDT_INFO* wdtinfo);

/*
**函数：enableWatchdog
**函数说明:设置看门狗，这个参数取决于struct __WDT_INFO 的nowayout的配置，如果nowayout为1时，此函数无效，任何时候都会返回成功，但是并不会做开启或者关闭动作
**参数：hlib 模块地址指针，envalue 1 打开 0 关闭 
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int enableWatchdog(HMODULE *hlib, unsigned char envalue);


/*
**函数：feedWatchdog
**函数说明:喂狗函数，如果在BIOS已开启看门狗，可不调用cfgWatchdog函数配置看门狗，并直接调用此函数并传入value这个看门狗的时间(必须)，如果已经调用了cfgWatchdog函数配置看门狗，以cfgWatchdog
**         的配置参数为准，不需要重新传入value这个看门狗的时间(这个参数此时无效)
**参数：hlib 模块地址指针，
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int feedWatchdog(HMODULE *hlib, unsigned char value);

//deinit system driver,exit
/*
**函数：libIoCtrlDeinit
**函数说明:卸载驱动及导入SDK动态库
**参数：hlib 模块地址指针
**返回值: 0 成功，非0失败
*/
extern "C" __declspec(dllexport) int libIoCtrlDeinit(HMODULE *hlib);

#endif