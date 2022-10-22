#include "Header.h"
#include <memory>
#include <vtkNew.h>

std::shared_ptr<VtkWindow> window;

#ifdef __cplusplus  

extern "C" {  // only need to export C interface if  
			  // used by C++ source code  
#endif  

	EXPORT(void) CreateInstance()
	{
		window = std::make_shared<VtkWindow>();
	}

	EXPORT(void) InitMyWindow(HWND hwnd)
	{ return window->InitMyWindow(hwnd); }

	EXPORT(void) AddCylinder()
	{
		return window->AddCylinder();
	}

	EXPORT(void) Refresh()
	{
		return window->Refresh();
	}

	EXPORT(void) CloseRender()
	{
		return window->CloseRender();
	}

#ifdef __cplusplus  
}

#endif  