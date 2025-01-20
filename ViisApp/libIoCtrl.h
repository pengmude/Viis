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
**������libIoCtrlInit
**����˵��:��ʼ������������SDK��̬��
**������hlib ģ���ַָ�룬bios_id ����BIOS ID�ţ�������BIOS setup�п��Բ鿴�õ���
**����ֵ: 0 �ɹ�����0ʧ�� ���ɹ�ʱ������HMODULE��ʵ��hlib
*/
extern "C" __declspec(dllexport) int libIoCtrlInit(HMODULE *hlib,char* bios_id);

//for gpio 
/*
**������setPinInOut
**����˵��:���ùܽŵ��������
**������hlib ģ���ַָ�룬index �ܽ���ţ��ο�GPIO_INDEXö�ٶ��壩��state �ܽ������������(�ο�GPIO_INOUT��ö�ٶ���)
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int setPinInOut(HMODULE *hlib, GPIO_INDEX index, GPIO_INOUT state);

/*
**������getPinLevel
**����˵��:��ȡ�ܽŵ��������״̬
**������hlib ģ���ַָ�룬index �ܽ���ţ��ο�GPIO_INDEXö�ٶ��壩��curstate �����Ĺܽ��������״̬,0Ϊ�͵�ƽ��1Ϊ�ߵ�ƽ
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int getPinLevel(HMODULE *hlib, GPIO_INDEX index, unsigned char* curstate);


/*
**������setPinLevel
**����˵��:���ùܽŵ��������״̬
**������hlib ģ���ַָ�룬index �ܽ���ţ��ο�GPIO_INDEXö�ٶ��壩��curstate ���ùܽ��������״̬,0Ϊ�͵�ƽ��1Ϊ�ߵ�ƽ(��Ϊ�Ĵ���״̬��ʵ�ʵ�·��״̬��ȡ�����ⲿ��·)
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int setPinLevel(HMODULE *hlib, GPIO_INDEX index, GPIO_LEVEL curstate);

//for watchdog
/*
**������cfgWatchdog
**����˵��:���ÿ��Ź�
**������hlib ģ���ַָ�룬iwdtinfo ���Ź�״̬���壬��ο�struct __WDT_INFO�Ķ���
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int cfgWatchdog(HMODULE *hlib, struct __WDT_INFO* wdtinfo);

/*
**������enableWatchdog
**����˵��:���ÿ��Ź����������ȡ����struct __WDT_INFO ��nowayout�����ã����nowayoutΪ1ʱ���˺�����Ч���κ�ʱ�򶼻᷵�سɹ������ǲ��������������߹رն���
**������hlib ģ���ַָ�룬envalue 1 �� 0 �ر� 
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int enableWatchdog(HMODULE *hlib, unsigned char envalue);


/*
**������feedWatchdog
**����˵��:ι�������������BIOS�ѿ������Ź����ɲ�����cfgWatchdog�������ÿ��Ź�����ֱ�ӵ��ô˺���������value������Ź���ʱ��(����)������Ѿ�������cfgWatchdog�������ÿ��Ź�����cfgWatchdog
**         �����ò���Ϊ׼������Ҫ���´���value������Ź���ʱ��(���������ʱ��Ч)
**������hlib ģ���ַָ�룬
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int feedWatchdog(HMODULE *hlib, unsigned char value);

//deinit system driver,exit
/*
**������libIoCtrlDeinit
**����˵��:ж������������SDK��̬��
**������hlib ģ���ַָ��
**����ֵ: 0 �ɹ�����0ʧ��
*/
extern "C" __declspec(dllexport) int libIoCtrlDeinit(HMODULE *hlib);

#endif