#pragma once
#include <vtkAutoInit.h>
#include <vtkActor.h>
#include <vtkCamera.h>
#include <vtkCylinderSource.h>
#include <vtkNamedColors.h>
#include <vtkNew.h>
#include <vtkPolyDataMapper.h>
#include <vtkProperty.h>
#include <vtkRenderWindow.h>
#include "export.h"
#include "ppl.h"

#include <vtkRenderWindowInteractor.h>
#include <vtkRenderer.h>
#include <vtkGenericOpenGLRenderWindow.h>
#include <vtkWin32OpenGLRenderWindow.h>

VTK_MODULE_INIT(vtkRenderingOpenGL2)
VTK_MODULE_INIT(vtkInteractionStyle)

class VtkWindow
{
public:
	VtkWindow() {}
	~VtkWindow() {}
public:
	void InitMyWindow(HWND hwnd)
	{
		//renderWindow->SetWindowId(hwnd);
		renderWindow->SetParentId((HWND)hwnd);
		renderer->SetRenderWindow(renderWindow);
		renderer->SetBackground(0.0, 0.0, 0.4);
		renderer->ResetCamera();
		renderWindow->AddRenderer(renderer);
		renderWindow->Render();
		interactor = renderWindow->GetInteractor();
		if (interactor == nullptr)
		{
			interactor = vtkSmartPointer<vtkRenderWindowInteractor>::New();
			renderWindow->SetInteractor(interactor);
		}
		renderWindow->SetSize(640, 480);
		interactor->Start();
	}

	void AddCylinder()
	{
		vtkNew<vtkCylinderSource> cylinder;
		cylinder->SetResolution(8);
		vtkNew<vtkPolyDataMapper> cylinderMapper;
		cylinderMapper->SetInputConnection(cylinder->GetOutputPort());
		actor = vtkSmartPointer<vtkActor>::New();
		actor->SetMapper(cylinderMapper);
		actor->RotateX(30.0);
		actor->RotateY(-45.0);

		renderer->AddActor(actor);
		renderer->ResetCamera();
		renderer->GetActiveCamera()->Zoom(1.5);

	}

	void Refresh()
	{
		renderWindow->Render();
	}

	void CloseRender()
	{
		if (interactor)
		{
			interactor->GetRenderWindow()->Finalize();
			interactor->TerminateApp();
		}
	}

private:
	vtkSmartPointer<vtkActor> actor;
	vtkSmartPointer<vtkRenderer> renderer = vtkSmartPointer<vtkRenderer>::New();
	vtkSmartPointer<vtkRenderWindowInteractor> interactor;
	vtkSmartPointer<vtkWin32OpenGLRenderWindow > renderWindow = vtkSmartPointer<vtkWin32OpenGLRenderWindow >::New();
};