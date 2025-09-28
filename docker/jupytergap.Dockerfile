FROM python:3.11-slim

WORKDIR /app

# Install system dependencies for GAP kernel
RUN apt-get update && apt-get install -y wget gap

# Install Python packages
RUN pip install --upgrade pip setuptools wheel
RUN pip install "notebook<7" jupyter
RUN pip install jupyter-kernel-gap

# Expose Jupyter
EXPOSE 8888

CMD ["jupyter", "notebook", "--ip=0.0.0.0", "--allow-root", "--NotebookApp.token=''", "--NotebookApp.password=''"]
