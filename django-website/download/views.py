from django.http import HttpResponse
from django.template import loader
from django.contrib.auth.models import User

def download(request):
    template = loader.get_template('download.html')
    context = {'pamount': User.objects.all().count()}
    return HttpResponse(template.render(context, request))
# Create your views here.
