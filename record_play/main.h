#ifndef RECDLL
#define RECDLL
#include <windows.h>
#define EXPORT extern "C" __declspec(dllexport)

#define IDC_RECORD_BEG                  1000
#define IDC_RECORD_END                  1001
#define IDC_PLAY_BEG                    1002
#define IDC_PLAY_PAUSE                  1003
#define IDC_PLAY_END                    1004
#define IDC_PLAY_REV                    1005
#define IDC_PLAY_REP                    1006
#define IDC_PLAY_SPEED                  1007

typedef struct {
	int sampleRate;
	int bitSample;
} RecordData;

TCHAR szAppName[] = TEXT("Record");
EXPORT void CALLBACK ReverseMemory(BYTE * pBuffer, int iLength);
EXPORT BOOL CALLBACK DlgProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
EXPORT HWND CALLBACK DlgShow(int sampleRate, int bitSample);
EXPORT PBYTE CALLBACK GetSaveBuffer();
EXPORT void CALLBACK SendSamples(byte s[], DWORD length);
EXPORT DWORD CALLBACK GetLength();
#endif