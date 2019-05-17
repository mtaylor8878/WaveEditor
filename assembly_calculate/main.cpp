#include "main.h"

int WINAPI DllMain(HINSTANCE hinstance, DWORD fdwReason, PVOID pvReserved) {
	return TRUE;
}

EXPORT void CALLBACK convolution_filter(FLOAT *filter, FLOAT *result, INT *samples, INT fSize, INT sSize) {
	__asm {
		PUSH EAX
		PUSH EBX
		PUSH ECX
		PUSH EDX
		PUSH ESI
		PUSH EDI

		MOV EDX, filter
		MOV ESI, samples
		MOV EDI, result
		MOV ECX, sSize
		MOV EBX, fSize

		XORPS XMM0, XMM0
		CVTSI2SS XMM0, [samples]
		SHUFPS XMM0, XMM0, 93h

		start: CMP ECX, 0
			   JZ end
			 
			SUB ECX, 4
		end:

		POP EDI
		POP ESI
		POP EDX
		POP ECX
		POP EBX
		POP EAX
	}
}
