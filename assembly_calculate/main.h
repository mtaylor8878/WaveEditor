#ifndef MAIN_H
#define MAIN_H
#include <windows.h>
#define EXPORT extern "C" __declspec(dllexport)

EXPORT void CALLBACK convolution_filter(FLOAT *filter, FLOAT *result, INT *samples, INT fSize, INT sSize);
#endif