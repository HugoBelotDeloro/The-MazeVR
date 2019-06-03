from django.http import HttpResponse
from django.template import loader
from django.contrib.auth.models import User


def stats(request):
    template = loader.get_template('stats.html')
    context = {}
    return HttpResponse(template.render(context, request))
# Create your views here.
